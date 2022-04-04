@feedback
Feature: ApprenticeSubmitsFeedback

Scenario: The apprentice has multiple potential training providers
	Given the apprentice has logged in
	And the apprentice has multiple training providers
	When accessing the index page
	Then the page content includes the following: Select training provider
#
#Scenario: The apprentice has completed there only apprenticeship more than 3 months ago
#	Given the apprentice has logged in
#	When accessing the index page
#	Then the recently completed message is displayed
#
#Scenario: The apprentice has completed there only apprenticeship is valid for feedback
#	Given the apprentice has logged in
#	When accessing the index page
#	Then the recently completed message is displayed