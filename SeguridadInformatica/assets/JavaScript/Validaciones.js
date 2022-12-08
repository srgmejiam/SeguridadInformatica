
function soloLetras(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    if (tecla == 8) return true;
    patron = /[A-Za-z]/;
    te = String.fromCharCode(tecla);
    return patron.test(te);
}

//Uso: onkeypress="return soloNumeros(event);" - Permite solo Números
function soloNumeros(evento) {
    var keynum = window.event ? window.event.keyCode : evento.which;
    if ((keynum == 8 || keynum == 48))
        return true;
    if (keynum <= 47 || keynum >= 58) return false;
    return /\d/.test(String.fromCharCode(keynum));
}

//Uso: onkeypress = "return blockCaracteresEspeciales(event)" - Validar Caracteres Especiales
function blockCaracteresEspeciales(e) {
    tecla = (document.all) ? e.keyCode : e.which;

    //Tecla de retroceso para borrar, siempre la permite
    if (tecla == 8) {
        return true;
    }

    //Anulamos el espcio en esta ocoación
    if (tecla == 32) {
        return false;
    }

    // Patron de entrada, en este caso solo acepta numeros y letras

    patron = /[A-Za-z0-9]/;
    tecla_final = String.fromCharCode(tecla);
    return patron.test(tecla_final);
}

//Uso: onkeypress = "return letrasNumerosGuion(event)"
function letrasNumerosGuion(e) {
    tecla = (document.all) ? e.keyCode : e.which;

   /* alert(tecla);*/

    //Tecla de retroceso para borrar, siempre la permite
    if (tecla == 8) {
        return true;
    }

    //Tecla guion simpre la permite
    if (tecla == 45) {
        return true;
    }

    //Anulamos el espcio en esta ocoación
    if (tecla == 32) {
        return false;
    }

    // Patron de entrada, en este caso solo acepta numeros y letras

    patron = /[A-Za-z0-9]/;
    tecla_final = String.fromCharCode(tecla);
    return patron.test(tecla_final);
}



