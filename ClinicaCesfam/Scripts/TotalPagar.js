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