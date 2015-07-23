using System;
using System.Collections.Generic;
using System.Linq;

namespace PeletonSoft.Tools.Model.NotifyChanged.Render
{
    public class RenderChangedDispatcher<TR, TS,  TD>
    {
        private readonly Dictionary<TR, Dictionary<TS, Func<TD>>> _subscribes = new Dictionary<TR, Dictionary<TS, Func<TD>>>();

        private Dictionary<TS, Func<TD>> GetSuscribeByResponder(TR responder)
        {
            if (!_subscribes.ContainsKey(responder))
            {
                _subscribes.Add(responder, new Dictionary<TS, Func<TD>>());
            }
            return _subscribes[responder];
        }

        public Action Subscribe(TS sender, TR responder, Func<TD> dataFunc)
        {
            var subscribes = GetSuscribeByResponder(responder);
            if (!subscribes.ContainsKey(sender))
            {
                subscribes.Add(sender, dataFunc);
            }
            else
            {
                subscribes[sender] = dataFunc;
            }
            RaiseRenderChanged(responder);
            return () => RaiseRenderChanged(responder);
            
        }

        public void Unsubscribe(TS sender, TR responder)
        {
            var subscribes = GetSuscribeByResponder(responder);
            if (subscribes.ContainsKey(sender))
            {
                subscribes.Remove(sender);
                RaiseRenderChanged(responder);
            }
            
        }


        public event RenderChangedEventHandler<IEnumerable<TD>> RenderChanged;

        private void RaiseRenderChanged(TR responder)
        {
            var renderData = GetSuscribeByResponder(responder).Values
                .Select(value => value());
            var args = new RenderChangedEventHandlerArgs<IEnumerable<TD>>(renderData);
            var handler = RenderChanged;
            if (handler != null)
            {
                handler(responder, args);
            }
        }

    }
}
