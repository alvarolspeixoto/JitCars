
$(document).ready(function () {
    // Função para filtrar carros
    function filtrarCarros() {
        var filtroTexto = $("#filtro").val().toLowerCase(); // Seleciona o valor da caixa de texto com ID "filtro"
        var carros = $(".card"); // Seleciona todos os elementos com a classe "card"

        // Itera sobre os carros
        carros.each(function () {
            var modeloCarro = $(this).data("modelo").toLowerCase(); // Obtém o valor do atributo "data-modelo" em minúsculas

            // Verifica se o modelo do carro inclui o texto do filtro
            if (modeloCarro.includes(filtroTexto)) {
                $(this).show(); // Exibe o carro
            } else {
                $(this).hide(); // Oculta o carro
            }
        });
    }

    // Chama a função de filtragem quando o valor do campo de filtro é alterado
    $("#filtro").on("input", filtrarCarros);
});
