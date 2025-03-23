document.addEventListener("DOMContentLoaded", function () {
    cargarAutores();

    document.getElementById("form-libro").addEventListener("submit", function (event) {
        event.preventDefault();
        agregarLibro();
    });
});

// 🔹 Cargar la lista de autores en la barra desplegable
function cargarAutores() {
    fetch("/Libro/GetAutores")
        .then(response => response.json())
        .then(autores => {
            let select = document.getElementById("autorLibro");
            select.innerHTML = "<option value=''>Seleccione un autor</option>";
            autores.forEach(autor => {
                select.innerHTML += `<option value="${autor.autorId}">${autor.nombre}</option>`;
            });
        })
        .catch(error => console.error("Error cargando autores:", error));
}

// 🔹 Enviar el formulario para registrar un libro
function agregarLibro() {
    let titulo = document.getElementById("tituloLibro").value;
    let autorId = document.getElementById("autorLibro").value;

    if (!titulo || !autorId) {
        alert("Por favor complete todos los campos.");
        return;
    }

    let libro = {
        titulo: titulo,
        autorId: parseInt(autorId)
    };

    fetch("/Libro/AddLibro", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(libro)
    })
        .then(response => response.json())
        .then(data => {
            alert(data.mensaje);
            document.getElementById("form-libro").reset();
        })
        .catch(error => console.error("Error agregando libro:", error));
}