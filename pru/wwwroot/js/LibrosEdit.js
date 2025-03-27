document.addEventListener("DOMContentLoaded", function () {
    const id = obtenerIdUrl();
    cargarAutores();
    cargarLibro(id);

    const form = document.getElementById("form-libro");
    form.addEventListener("submit", function (e) {
        e.preventDefault();
        actualizarLibro(id);
    });
});

function obtenerIdUrl() {
    const params = new URLSearchParams(window.location.search);
    return params.get("id");
}

function cargarAutores() {
    fetch("/Libro/GetAutores")
        .then(response => response.json())
        .then(autores => {
            const select = document.getElementById("autorLibro");
            select.innerHTML = "";
            autores.forEach(autor => {
                const option = document.createElement("option");
                option.value = autor.autorId;
                option.textContent = autor.nombre;
                select.appendChild(option);
            });
        })
        .catch(error => console.error("Error cargando autores:", error));
}

function cargarLibro(id) {
    fetch(`/Libro/GetLibroById?id=${id}`)
        .then(response => response.json())
        .then(libro => {
            document.getElementById("tituloLibro").value = libro.titulo;
            document.getElementById("autorLibro").value = libro.autorId;
        })
        .catch(error => console.error("Error cargando libro:", error));
}

function actualizarLibro(id) {
    const titulo = document.getElementById("tituloLibro").value;
    const autorId = document.getElementById("autorLibro").value;

    fetch(`/Libro/ActualizarLibro?id=${id}`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ titulo: titulo, autorId: parseInt(autorId) })
    })
        .then(response => {
            if (response.ok) {
                alert("Libro actualizado correctamente");
                window.location.href = "/Home/Index"; // o donde quieras redirigir
            } else {
                alert("Error al actualizar el libro");
            }
        })
        .catch(error => console.error("Error actualizando libro:", error));
}