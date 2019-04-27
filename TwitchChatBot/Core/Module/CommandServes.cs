using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TwitchLib.Client.Events;
using TwitchChatBot.Core.Interface;
using TwitchLib.Client;
using TwitchChatBot.Core.Atribute;

namespace TwitchChatBot.Core.Module
{
    public class CommandServes
    {
        Dictionary<string, System.Reflection.MethodInfo> _methods = new Dictionary<string, System.Reflection.MethodInfo>();
        Type ClassType;
        Object MyClassObject;

        public CommandServes()
        {
            ClassType = null;
            MyClassObject = null;
        }

        public void GetClass<T>()
        {
            ClassType = typeof(T);
            var constr = ClassType.GetConstructors()[0];
            MyClassObject = constr.Invoke(new object[] { });

            var Atribute = typeof(TwitchChatBot.Atribute.Command);

            foreach (var method in ClassType.GetMethods())
                foreach (var atr in method.CustomAttributes)
                    if (Atribute == atr.AttributeType)
                    {
                        string namecommand = atr.ConstructorArguments[0].ToString().ToLower();
                        namecommand = namecommand.Substring(1, namecommand.Length - 2);
                        _methods.Add(namecommand, method);
                        break;
                    }
        }

        public PreconditionResult Invoke(int argpos, TwitchClient cl, OnMessageReceivedArgs e)
        {
            if (_methods.Count == 0 || MyClassObject == null || ClassType == null)
                return PreconditionResult.Unsuccessfully;

            int poz = e.ChatMessage.Message.IndexOf(' ');
            poz = poz < 0 ? e.ChatMessage.Message.Length - 1 : poz - 1;
            string NameMethods = e.ChatMessage.Message.Substring(1, poz).ToLower();

            ((ICommandContext)MyClassObject).SetContext(e, cl);
            List<object> _params;

            foreach (var method in _methods)
                if (method.Key == NameMethods)
                {
                    var param = method.Value.GetParameters();
                    int count = param.Count();
                    GetParam(argpos, e.ChatMessage.Message, out _params, method.Value.GetParameters());
                    if (count == _params.Count)
                    {
                        bool res = true;
                        foreach(var atr in method.Value.GetCustomAttributes())
                        {
                            var attribute = atr as TwitchChatBot.Core.Atribute.PreconditionAttribute;//преобразование типов
                            if (attribute == null)
                                continue;
                            PreconditionResult result = attribute.CheckPermissionsAsync(e);
                            if (result == PreconditionResult.Unsuccessfully)
                                res &= false;
                        }

                        if (!res)
                            return PreconditionResult.Unsuccessfully;
                        
                        method.Value.Invoke(MyClassObject, _params.ToArray());
                    }
                    break;
                }

            return PreconditionResult.Successfully;
        }

        private void GetParam(int argpos, string name, out List<object> _params, ParameterInfo[] _methodparams)
        {
            _params = new List<object>();

            if (name.IndexOf(' ') < 0 || _methodparams.Count() == 0)
                return;

            int endpoz = name.IndexOf(' ');
            endpoz = endpoz < 0 ? name.Length : endpoz + 1;
            name = name.Substring(endpoz, name.Length - endpoz);
            endpoz = 0;
            int i = 0;
            int count = _methodparams.Length;

            while (name != "" && i < count)
            {
                int nextsym = name.IndexOf(' ');
                int nextsymC = name.IndexOf('\"');

                if(nextsymC < nextsym)
                {
                    int num = nextsymC + 1;
                    for (; num < name.Length && name[num] != '\"'; num++) ;
                    string par = name.Substring(nextsymC + 1, num - nextsymC - 1);
                    _params.Add(par);
                    i++;
                    continue;
                }

                nextsym = nextsym < 0 ? name.Length : nextsym + 1;
                string param = name.Substring(endpoz, nextsym != name.Length ? nextsym - 1 : nextsym);



                switch(_methodparams[i].ParameterType.Name)
                {
                    case "String":
                        {
                            _params.Add(param);
                            i++;
                            break;
                        }
                    case "Int16":
                        {
                            if (Int16.TryParse(param, out short par))
                            {
                                _params.Add(par);
                                i++;
                                break;
                            }
                            else throw new Exception("Не совпадают параметры!");
                        }
                    case "Int32":
                        {
                            if (Int32.TryParse(param, out int par))
                            {
                                _params.Add(par);
                                i++;
                                break;
                            }
                            else throw new Exception("Не совпадают параметры!");
                        }
                    case "Int64":
                        {
                            if (Int64.TryParse(param, out long par))
                            {
                                _params.Add(par);
                                i++;
                                break;
                            }
                            else throw new Exception("Не совпадают параметры!");
                        }
                    case "Double":
                        {
                            if (Double.TryParse(param, out double par))
                            {
                                _params.Add(par);
                                i++;
                                break;
                            }
                            else throw new Exception("Не совпадают параметры!");
                        }
                    case "Boolean":
                        {
                            if (Boolean.TryParse(param, out bool par))
                            {
                                _params.Add(par);
                                i++;
                                break;
                            }
                            else throw new Exception("Не совпадают параметры!");
                        }
                    case "Object":
                        {
                            _params.Add((Object)param);
                            i++;
                            break;
                        }
                    default:
                        {
                            throw new Exception($"Не совпадают параметры! {_methodparams[i].ParameterType.Name}");
                        }
                }

                name = name.Substring(nextsym, name.Length - nextsym);
            }
        }
    }
}
