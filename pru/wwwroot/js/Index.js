
document.addEventListener("DOMContentLoaded", function () {
    cargarAutores(); // Cargar autores al inicio

    document.getElementById("form-autor").addEventListener("submit", function (event) {
        event.preventDefault(); // Evita recargar la página
        agregarAutor();
    });
});

// trae los datos del controlador
function cargarAutores() {
    fetch("/Libro/GetLibros")
        .then(response => response.json())
        .then(autores => {
            let tbody = document.getElementById("tabla-Libros");
            tbody.innerHTML = ""; // Limpiar la tabla
            autores.forEach(libro => {
                let fila = `<tr>
                                <td>${libro.titulo}</td>
                            </tr>`;
                tbody.innerHTML += fila;
            });
        })
        .catch(error => console.error("Error cargando autores:", error));
}
