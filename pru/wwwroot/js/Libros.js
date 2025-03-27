document.addEventListener("DOMContentLoaded", function () {
    cargarAutores();
    cargarLibros();

    const form = document.getElementById("form-libro");
    if (form) {
        form.addEventListener("submit", function (event) {
            event.preventDefault();
            agregarLibro();
        });
    }
});

// 🔹 Cargar la lista de autores en el select
function cargarAutores() {
    fetch("/Libro/GetAutores")
        .then(response => response.json())
        .then(autores => {
            const select = document.getElementById("autorLibro");
            if (!select) return;
            select.innerHTML = "<option value=''>Seleccione un autor</option>";
            autores.forEach(autor => {
                const option = document.createElement("option");
                option.value = autor.autorId;
                option.textContent = autor.nombre;
                select.appendChild(option);
            });
        })
        .catch(error => console.error("Error cargando autores:", error));
}

// 🔹 Cargar la lista de libros en la tabla
function cargarLibros() {
    fetch("/Libro/GetLibros")
        .then(response => response.json())
        .then(libros => {
            const tbody = document.getElementById("tabla-libros");
            if (!tbody) return;
            tbody.innerHTML = "";

            libros.forEach(libro => {
                const fila = document.createElement("tr");

                fila.innerHTML = `
                    <td>${libro.titulo}</td>
                    <td>${libro.autorNombre}</td>
                    <td>
                        <button onclick="eliminarLibro(${libro.id})">Eliminar</button>
                    </td>
                `;

                tbody.appendChild(fila);
            });
        })
        .catch(error => console.error("Error cargando libros:", error));
}

// 🔹 Agregar un nuevo libro
function agregarLibro() {
    const titulo = document.getElementById("tituloLibro").value;
    const autorId = document.getElementById("autorLibro").value;

    if (!titulo || !autorId) {
        alert("Por favor complete todos los campos.");
        return;
    }

    const libro = {
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
            cargarLibros();
        })
        .catch(error => console.error("Error agregando libro:", error));
}

// 🔹 Eliminar un libro
function eliminarLibro(id) {
    if (confirm("¿Estás seguro que quieres eliminar este libro?")) {
        fetch(`/Libro/EliminarLibro?id=${id}`, {
            method: "DELETE"
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error("Error al eliminar el libro");
                }
                alert("Libro eliminado correctamente");
                window.location.reload(); // 🔥 Forzar recarga de la página
            })
            .catch(error => {
                console.error("Error eliminando libro:", error);
                alert("Ocurrió un error al eliminar el libro");
            });
    }
}