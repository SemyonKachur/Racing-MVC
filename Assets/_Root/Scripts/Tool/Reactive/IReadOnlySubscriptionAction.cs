using System;

namespace Tool
{
    internal interface IReadOnlySubscriptionAction
    {
        void SubscribeOnChange(Action subscriptionAction);
        void UnSubscribeOnChange(Action subscriptionAction);
    }
}
