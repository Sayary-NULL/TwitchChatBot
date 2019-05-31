using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TwitchCoreAPI.Core.Interface;
using TwitchCoreAPI.Core.Result;
using TwitchLib.Client;
using TwitchLib.Client.Events;

namespace TwitchCoreAPI.Core.Module
{
    public class CommandServes
    {
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
        }

        public ErrorsReturnType Invoke(int argpos, TwitchClient cl, OnMessageReceivedArgs e)
        {
            var CommantAttribute = typeof(TwitchCoreAPI.Core.Attribute.Command);
            var AliasAttribute = typeof(TwitchCoreAPI.Core.Attribute.AliasAttribute);
            var PrecondAttribute = typeof(TwitchCoreAPI.Core.Attribute.PreconditionAttribute);

            int poz = e.ChatMessage.Message.IndexOf(' ');
            poz = poz < 0 ? e.ChatMessage.Message.Length - 1 : poz - 1;
            string NameMethods = e.ChatMessage.Message.Substring(1, poz).ToLower();

            List<object> _params;

            foreach (var method in ClassType.GetMethods())
            {
                bool CommandName = false;
                bool AliasName = false;
                bool PreconditionCon = true;
                PreconditionResult Presult = new PreconditionResult();

                foreach (var attrib in method.GetCustomAttributes())
                {
                    var type = attrib.GetType();
                    if(type == CommantAttribute)
                    {
                        var at = attrib as TwitchCoreAPI.Core.Attribute.Command;
                        if (at.NameCommand == NameMethods)
                            CommandName |= true;
                        else CommandName &= false;
                    }
                    else if(type == AliasAttribute)
                    {
                        var at = attrib as TwitchCoreAPI.Core.Attribute.AliasAttribute;
                        if (at.NameAlias == NameMethods)
                            AliasName = true;
                    }
                    else if(type.BaseType == PrecondAttribute)
                    {
                        PreconditionCon = false;
                        var at = attrib as TwitchCoreAPI.Core.Attribute.PreconditionAttribute;
                        Presult = at.CheckPermissions(e);
                        if (Presult.res == PreconditionResult.Result.Successfully)
                            PreconditionCon = true;
                    }
                }

                if((CommandName || AliasName) && PreconditionCon)
                {
                    ErrorsType result = GetParam(argpos, e.ChatMessage.Message, out _params, method.GetParameters());
                    if (result != ErrorsType.Successful && result != ErrorsType.ObjectNotFound)
                        return new ErrorsReturnType(ErrorsType.ParseFailed, $"Не соответствие параметров при передаче в функцию. Строка: \"{e.ChatMessage.Message}\"");

                    if (_params.Count == method.GetParameters().Count())
                    {
                        ((ICommandContext)MyClassObject).SetContext(e, cl);
                        method.Invoke(MyClassObject, _params.ToArray());
                        return new ErrorsReturnType(ErrorsType.Successful, "");
                    }
                    else return new ErrorsReturnType(ErrorsType.BadArgCount, $"Не соответсвие кол-ва аргументов функции [{_params.Count}]:[{method.GetParameters().Count()}], сообщение : \"{e.ChatMessage.Message}\"");
                }

                if(!PreconditionCon && (CommandName || AliasName))
                {
                    return new ErrorsReturnType(ErrorsType.UnmetPrecondition, $"{Presult.ErrorResult}'");
                }
            }

            return new ErrorsReturnType(ErrorsType.ObjectNotFound, $"Не найдена функция \"{NameMethods}\"");
        }

        private ErrorsType GetParam(int argpos, string name, out List<object> _params, ParameterInfo[] _methodparams)
        {
            _params = new List<object>();

            if (_methodparams.Count() == 0)
                return ErrorsType.Successful;

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
                            else return ErrorsType.ParseFailed;
                        }
                    case "Int32":
                        {
                            if (Int32.TryParse(param, out int par))
                            {
                                _params.Add(par);
                                i++;
                                break;
                            }
                            else return ErrorsType.ParseFailed;
                        }
                    case "Int64":
                        {
                            if (Int64.TryParse(param, out long par))
                            {
                                _params.Add(par);
                                i++;
                                break;
                            }
                            else return ErrorsType.ParseFailed;
                        }
                    case "Double":
                        {
                            if (Double.TryParse(param, out double par))
                            {
                                _params.Add(par);
                                i++;
                                break;
                            }
                            else return ErrorsType.ParseFailed;
                        }
                    case "Boolean":
                        {
                            if (Boolean.TryParse(param, out bool par))
                            {
                                _params.Add(par);
                                i++;
                                break;
                            }
                            else return ErrorsType.ParseFailed;
                        }
                    case "Object":
                        {
                            _params.Add((Object)param);
                            i++;
                            break;
                        }
                    default:
                        {
                            return ErrorsType.ParseFailed;
                        }
                }

                name = name.Substring(nextsym, name.Length - nextsym);
            }

            return ErrorsType.Successful;
        }
    }
}
