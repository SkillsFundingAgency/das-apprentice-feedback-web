$(function () {

    $('input[type=checkbox]').on('change', function () {

        if (this.checked) {

            /* For go-live there are 16 reason attributes. 
             * So the "none" option has array index 15
             * This is quite brittle and one to watch out for if ever the list of attributes changes in the future
             */
            if (this.id == 'ReasonAttributes_15__Value') {
                $('#ReasonAttributes_0__Value').prop('checked', false);
                $('#ReasonAttributes_1__Value').prop('checked', false);
                $('#ReasonAttributes_2__Value').prop('checked', false);
                $('#ReasonAttributes_3__Value').prop('checked', false);
                $('#ReasonAttributes_4__Value').prop('checked', false);
                $('#ReasonAttributes_5__Value').prop('checked', false);
                $('#ReasonAttributes_6__Value').prop('checked', false);
                $('#ReasonAttributes_7__Value').prop('checked', false);
                $('#ReasonAttributes_8__Value').prop('checked', false);
                $('#ReasonAttributes_9__Value').prop('checked', false);
                $('#ReasonAttributes_10__Value').prop('checked', false);
                $('#ReasonAttributes_11__Value').prop('checked', false);
                $('#ReasonAttributes_12__Value').prop('checked', false);
                $('#ReasonAttributes_13__Value').prop('checked', false);
                $('#ReasonAttributes_14__Value').prop('checked', false);
            }
            else {
                $('#ReasonAttributes_15__Value').prop('checked', false);
            }
        }
    });
});