function validate() {
    var size = 50000;
    var file_size = document.getElementById("imagen").files[0].size;
    if (file_size >= size) {
        alert('El tamaño de la imagen debe ser menor a 5MB');
        return false;
    }
    var type = 'image/jpeg';
    var file_type = document.getElementById('imagen').files[0].type;
    if (file_type != type) {
        alert('Formato no soportado, solo formatos JPG y JPGE');
        return false;
    }
}