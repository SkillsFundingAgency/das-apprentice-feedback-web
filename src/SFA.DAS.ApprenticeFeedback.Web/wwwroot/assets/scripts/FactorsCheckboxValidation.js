$(function () {

    $('input[type=checkbox]').on('change', function () {

        if (this.checked) {

            if (this.id == 'IncompletionFactor_None') {
                $('#IncompletionFactor_Caring').prop('checked', false);
                $('#IncompletionFactor_Family').prop('checked', false);
                $('#IncompletionFactor_Financial').prop('checked', false);
                $('#IncompletionFactor_Mental').prop('checked', false);
                $('#IncompletionFactor_Physical').prop('checked', false);
            }
            else {
                $('#IncompletionFactor_None').prop('checked', false);
            }
        }
    });
});