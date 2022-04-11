using Moq;
using NUnit.Framework;
using SFA.DAS.ApprenticeFeedback.Application.Services;
using SFA.DAS.ApprenticeFeedback.Application.Settings;
using SFA.DAS.ApprenticeFeedback.Domain.Interfaces;
using SFA.DAS.ApprenticeFeedback.Domain.Models.Feedback;
using System;
using System.Collections;

namespace SFA.DAS.ApprenticeFeedback.Application.UnitTests.Services
{
    public class ApprenticeFeedbackServiceTests
    {
        public class GetFeedbackEligibility
        {
            private readonly Mock<IApprenticeFeedbackApi> _mockApiClient;
            private readonly Mock<IDateTimeProvider> _dateTimeProvider;

            public GetFeedbackEligibility()
            {
                _mockApiClient = new Mock<IApprenticeFeedbackApi>();
                _dateTimeProvider = new Mock<IDateTimeProvider>();
                _dateTimeProvider.SetupGet(m => m.UtcNow).Returns(DateTime.UtcNow);
            }

            public static IEnumerable GetFeedbackEligibilityTestCases
            {
                get
                {
                    yield return new TestCaseData(
                        new Apprenticeship
                        {
                            LarsCode = 1,
                            StartDate = DateTime.Now.AddMonths(-1),
                        },
                        new FeedbackSettings()
                        {
                            InitialDenyPeriodDays = 61,
                            FinalAllowPeriodDays = 92,
                            RecentDenyPeriodDays = 14
                        }
                    ).Returns(FeedbackEligibility.Deny_TooSoon)
                    .SetName("TestFeedbackEligibility-RecentIsNotOk");

                    yield return new TestCaseData(
                        new Apprenticeship
                        {
                            LarsCode = 1,
                            StartDate = DateTime.Now.AddMonths(-2).AddDays(-2),
                        },
                        new FeedbackSettings()
                        {
                            InitialDenyPeriodDays = 61,
                            FinalAllowPeriodDays = 92,
                            RecentDenyPeriodDays = 14
                        }
                    ).Returns(FeedbackEligibility.Allow)
                    .SetName("TestFeedbackEligibility-RecentIsOk");

                    yield return new TestCaseData(
                        new Apprenticeship
                        {
                            LarsCode = 2,
                            StartDate = DateTime.Now.AddMonths(-6),
                            EndDate = DateTime.Now.AddMonths(-4),
                            Status = ApprenticeshipStatus.Pass
                        },
                        new FeedbackSettings()
                        {
                            InitialDenyPeriodDays = 61,
                            FinalAllowPeriodDays = 92,
                            RecentDenyPeriodDays = 14
                        }
                    ).Returns(FeedbackEligibility.Deny_TooLateAfterPassing)
                    .SetName("TestFeedbackEligibility-PassedTooLongAgoIsNotOk");

                    yield return new TestCaseData(
                        new Apprenticeship
                        {
                            LarsCode = 2,
                            StartDate = DateTime.Now.AddMonths(-6),
                            EndDate = DateTime.Now.AddMonths(-3),
                            Status = ApprenticeshipStatus.Pass
                        },
                        new FeedbackSettings()
                        {
                            InitialDenyPeriodDays = 61,
                            FinalAllowPeriodDays = 92,
                            RecentDenyPeriodDays = 14
                        }
                    ).Returns(FeedbackEligibility.Allow)
                    .SetName("TestFeedbackEligibility-PassedTooLongAgoIsOk");

                    // @Todo: remaining test cases
                }
            }

            [TestCaseSource(nameof(GetFeedbackEligibilityTestCases))]
            public FeedbackEligibility TestFeedbackEligibility(Apprenticeship apprenticeship, FeedbackSettings settings)
            {
                var serviceUnderTest = new ApprenticeFeedbackService(_mockApiClient.Object, _dateTimeProvider.Object, settings);
                return serviceUnderTest.GetFeedbackEligibility(apprenticeship).feedbackEligibility;
            }
        }
    }
}
