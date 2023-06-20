//Obtiene el valor total en tiempo real
$(document).ready(function () {
    // Se crea la variable cantidad por el numero de cantidades que ingreso el usuario
    var cantidadInput = $("#cantidad");
    // Se crea la variable total donde se almacenara el valor total final
    var totalOutput = $("#total");

    // Registrar el evento 'input' en el campo de cantidad
    cantidadInput.on("input", function () {
        //Obtiene el valor ingresado en la cantidad
        var cantidad = parseFloat(cantidadInput.val());

        //Calcula el valor total
        var total = precioMedicamento * cantidad;

        // Muestra el valor total en formato de texto(total)
        totalOutput.text(total);

        // Actualiza el valor del campo oculto "totalHidden"
        var totalHiddenInput = $("#totalHidden");
        totalHiddenInput.val(total);
    });
});


//Obtiene el nombre y apellido en tiempo real
$(document).ready(function () {
    // Seleciona el value, de la opcion selecionada en el <select>, Registra el evento para el cambio en el select
    $('#id_paciente').change(function () {
        var selectedPacienteId = $(this).val();

        // Realizar una llamada Ajax para obtener el nombre y apellido del paciente
        $.ajax({
            url: '/ReservaPago/ObtenerNombreApellido',
            type: 'GET',
            data: { id_paciente: selectedPacienteId },
            success: function (data) {
                // Actualiza el texto en tiempo real
                $('#nombreApellido').text(data);
                // Actualiza el valor del campo oculto "usuarioReserva"
                var usuarioReservaInput = $("#usuarioReserva");
                usuarioReservaInput.val(data);
            },
            // Resaltara una alerta si no se encuentra en nombre y apellido
            error: function () {
                alert('Ocurrió un error al obtener el nombre y apellido del paciente.');
            }
        });
    });
});


