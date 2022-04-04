using SFA.DAS.ApprenticeFeedback.Web.AcceptanceTests.Hooks;

namespace SFA.DAS.ApprenticeFeedback.Web.AcceptanceTests
{
    public class StepsBase
    {
        public StepsBase(TestContext testContext)
        {
            var hook = testContext.Web.ActionResultHook;

            if (hook != null && testContext.ActionResult == null)
            {
                testContext.ActionResult = new TestActionResult();
                hook.OnProcessed = (actionResult) => { testContext.ActionResult.SetActionResult(actionResult); };
                hook.OnErrored = (ex, actionResult) => { testContext.ActionResult.SetException(ex); };
            }
        }
    }
}
