document.addEventListener("DOMContentLoaded", function () {
    document.getElementById("form-autor").addEventListener("submit", function (event) {
        event.preventDefault();

        let nombre = document.getElementById("nombreAutor").value.trim();
        if (nombre === "") {
            alert("El nombre no puede estar vacío.");
            return;
        }

        fetch("/Autor/AddAutor", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ nombre: nombre })
        })
            .then(response => response.json())
            .then(nuevoAutor => {
                alert("Autor agregado correctamente!");
                document.getElementById("nombreAutor").value = "";
                window.location.reload(); // Opcional, para que recargue la lista si tienes tabla
            })
            .catch(error => console.error("Error al agregar autor:", error));
    });
});