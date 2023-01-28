﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuncAndActionDelegatesInCsharp
{
    public class StateResult 
    { 
        public object? result { get; set; }
        public string? LastFailedOperationName { get; set; }
        public bool HasErrors { get; set; } }

    public class ContinueExecutionUsingFunc
    {
        #region "Operation"
        public static StateResult Operation1(object u)
        {
            var result = new StateResult(); try
            { 
                //Do some work on object u 
                //call next operation
                result = Operation2(u); 
            } catch(Exception e) 
            { 
                Console.WriteLine(e.Message);
                result.HasErrors = true;
                result.LastFailedOperationName = nameof(Operation1); 
            } 
            return result; 
        }

        public static StateResult Operation2(object u)
        {
            var result = new StateResult(); try
            {
                //Do some work on object u 
                //call next operation
                result = Operation3(u);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                result.HasErrors = true;
                result.LastFailedOperationName = nameof(Operation2);
            }
            return result;
        }

        public static StateResult Operation3(object u)
        {
            var result = new StateResult(); try
            {
                //Do some work on object u 
                //call next operation
                result = Operation4(u);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                result.HasErrors = true;
                result.LastFailedOperationName = nameof(Operation3);
            }
            return result;
        }

        public static StateResult Operation4(object u)
        {
            var result = new StateResult(); try
            {
                //Do some work on object u 

                //Last Operation
                result.result = u;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                result.HasErrors = true;
                result.LastFailedOperationName = nameof(Operation4);
            }
            return result;
        }

        #endregion 

        public static StateResult ContinueExcution(string lastFailedPperationName, object u)
        { 
            Dictionary<string, Func<object, StateResult>> operations 
                = new Dictionary<string, Func<object, StateResult>> 
                    { 
                        { nameof(Operation1), Operation1 },
                        { nameof(Operation2), Operation2 },
                        { nameof(Operation3), Operation3 },
                        { nameof(Operation4), Operation4 }
                    };
            var stateResult = new StateResult();
            foreach (var pair in operations) 
            { 
                if (pair.Key != lastFailedPperationName)
                    continue; stateResult = pair.Value.Invoke(u); 
            } 
            return stateResult; 
        }

    }
}
