$(function () {

    hideError();

    $('input[type=checkbox]').on('change', function () {
        
        var factorChosen = $('#IncompletionFactor_Caring').is(':checked')
            || $('#IncompletionFactor_Family').is(':checked')
            || $('#IncompletionFactor_Financial').is(':checked')
            || $('#IncompletionFactor_Mental').is(':checked')
            || $('#IncompletionFactor_Physical').is(':checked')
            ;

        var noneChosen = $('#IncompletionFactor_Other').is(':checked');

        if ((factorChosen && noneChosen) || (!factorChosen && !noneChosen)) {
            showError();
        }
        else {
            hideError();
        }
    });

    function hideError() {
        $('#error-summary').hide();
        $('#factors').removeClass("govuk-form-group--error");
        $('#factors-error').hide();
    }

    function showError() {
        $('#error-summary').show();
        $('#factors').addClass("govuk-form-group--error");
        $('#factors-error').show();
    }
});