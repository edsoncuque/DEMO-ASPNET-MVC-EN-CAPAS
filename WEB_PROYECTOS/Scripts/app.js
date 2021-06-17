// metodos jquery
function LoadingOverlayShow(id) {
    $(id).LoadingOverlay("show", {
        color: "rgba(255,255,255,0.5)",
        image: "/Content/Loading_2.gif",
        imageResizeFactor: 0.6
    });
}

function LoadingOverlayHide(id) {
    $(id).LoadingOverlay("hide");
}

function ValidarFechas(dateIni, dateFin) {
    var _dateIni = new Date(dateIni);
    var _dateFin = new Date(dateFin);

    if (_dateFin < _dateIni) {
        return false;
    }
    else {
        return true;
    }

}

function getDepartamentos(myCallback) {
    $.ajax({
        type: "GET",
        url: '/departamento/getdepartamentos',
        dataType: "json",
        success: function (result) {
            $.each(result.data, function (key, item) {
                $("#Departamentoid").append('<option value=' + item.Departamentoid + '>' + item.NombreDepartamento + '</option>');
            });
            if (myCallback != undefined)
                return myCallback(result.data);
        },
        error: function (data) {
            alert('error');
        }

    });
}

function getProyectos(myCallback) {
    $.ajax({
        type: "GET",
        url: '/proyecto/listarproyectos',
        dataType: "json",
        success: function (result) {
            $.each(result.data, function (key, item) {
                $("#Proyectoid").append('<option value=' + item.Proyectoid + '>' + item.NombreProyecto + '</option>');
            });
            if(myCallback != undefined)
                return myCallback(result.data);
        },
        error: function (data) {
            alert('error');
        }

    });
}

function getEmpleados(myCallback) {
    $.ajax({
        type: "GET",
        url: '/empleado/listarempleados',
        dataType: "json",
        success: function (result) {
            $.each(result.data, function (key, item) {
                $("#Empleadoid").append('<option value=' + item.Empleadoid + '>' + item.Apellidos +','+ item.Nombres + '</option>');
            });
            if (myCallback != undefined)
                return myCallback(result.data);
        },
        error: function (data) {
            alert('error');
        }

    });
}