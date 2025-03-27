document.addEventListener("DOMContentLoaded", function () {
    cargarlibros();
});

function cargarlibros() {
    fetch("/Home/GetLibros")
        .then(response => response.json())
        .then(libros => {
            let tbody = document.getElementById("tabla-Libros");
            tbody.innerHTML = "";

            libros.forEach(libro => {
                let fila = `<tr>
                    
                    <td>${libro.titulo}</td>
                    <td>
                        <button class="btn btn-primary" onclick="editarLibro(${libro.id})">Editar</button>
                        <button class="btn btn-danger" onclick="eliminarLibro(${libro.id})">Eliminar</button>
                    </td>
                </tr>`;
                tbody.innerHTML += fila;
            });
        })
        .catch(error => console.error("Error cargando libros:", error));
}

function editarLibro(id) {
    window.location.href = `/Home/LibrosEdit?id=${id}`;
}

function eliminarLibro(id) {
    if (confirm("¿Estás seguro que quieres eliminar este libro?")) {
        // Aquí haces un fetch para eliminarlo
        alert("Libro eliminado con ID: " + id);
        // Luego recargas la tabla
        // fetch('/Home/EliminarLibro/' + id, { method: 'DELETE' })....
        // cargarlibros();
    }
}