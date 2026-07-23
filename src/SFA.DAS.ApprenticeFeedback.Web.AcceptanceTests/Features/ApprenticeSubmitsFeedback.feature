@feedback
Feature: ApprenticeSubmitsFeedback

Scenario: The apprentice has multiple potential training providers so ask them to select one
	Given the apprentice has logged in
	And the apprentice has multiple training providers
	When accessing the index page
	Then the page content includes the following: Select a training provider