﻿using CtrlVAF.Models;
using MFiles.VAF;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CtrlVAF.Core
{
    public abstract class Dispatcher: Dispatcher_Common, IDispatcher
    {
        public abstract void Dispatch(params ICtrlVAFCommand[] commands);

        public void Dispatch(Action<Exception> exceptionHandler, params ICtrlVAFCommand[] commands)
        {
            IncludeAssemblies(Assembly.GetCallingAssembly());

            try
            {
                Dispatch(commands);
            }
            catch (Exception ex)
            {
                if (exceptionHandler != null)
                    exceptionHandler(ex);
                else
                    throw ex;
            }

        }

        /// <summary>
        /// Executes logic for an array of types, usually supplied by <see cref="GetTypes"/>
        /// </summary>
        /// <param name="types">a list of types which can be instantiated and will behave as expected</param>
        /// <returns>The result or <see cref="default"/> for no result.</returns>
        protected internal abstract void HandleConcreteTypes(IEnumerable<Type> types, params ICtrlVAFCommand[] commands);

        /// <summary>
        /// Creates an implementation of <see cref="IEventHandlerMethodInfo"/> for routing the event.
        /// </summary>
        /// <returns></returns>
        public IEventHandlerMethodInfo CreateEventHandlerMethodInfo() => new CtrlVAF.Core.Events.EventHandlerMethodInfo(this);
    }

    /// <summary>
    /// Abstract class every dispatcher should inherit from. 
    /// </summary>
    /// <typeparam name="TReturn">The return type of the Dispatch method. If no return is expected this should be type object.</typeparam>
    public abstract class Dispatcher<TReturn> : Dispatcher_Common, IDispatcher<TReturn>
    {
        /// <inheritdoc/>
        public abstract TReturn Dispatch(params ICtrlVAFCommand[] commands);

        /// <summary>
        /// Tries to execute the commands. On an exception, stops execution and handles the exception. If the 
        /// </summary>
        /// <param name="exceptionHandler"></param>
        /// <param name="commands"></param>
        /// <returns>an object of <see cref="TReturn"/> or null if an exception occured</returns>
        public TReturn Dispatch(Action<Exception> exceptionHandler, params ICtrlVAFCommand[] commands)
        {
            IncludeAssemblies(Assembly.GetCallingAssembly());

            try
            {
                return Dispatch(commands);
            }
            catch (Exception ex)
            {
                if (exceptionHandler != null)
                    exceptionHandler(ex);
                else
                    throw ex;
            }
            return default;

        }

        /// <summary>
        /// Executes logic for an array of types, usually supplied by <see cref="GetTypes"/>
        /// </summary>
        /// <param name="types">a list of types which can be instantiated and will behave as expected</param>
        /// <returns>The result or <see cref="default"/> for no result.</returns>
        protected internal abstract TReturn HandleConcreteTypes(IEnumerable<Type> types, params ICtrlVAFCommand[] commands);
    }
}
