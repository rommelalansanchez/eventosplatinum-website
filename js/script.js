// Add custom JavaScript here
function userScroll() {
  const navbar = document.querySelector(".navbar");
  const navlinks = document.querySelectorAll(".nav-link");

  const navbarToggler = document.querySelector(".navbar-toggler");
  const navbarToglerIcon = document.querySelector(".menu-toggler-icon");

  window.addEventListener("scroll", () => {
    if (window.scrollY > 600) {
      navbar.classList.add("bg-primary");

      const navlinkList = [...navlinks].map((el) => {
        el.classList.add("navbar-link-secondary");
      });
      navbarToggler.classList.remove("navbar-toggler-primary");
      navbarToglerIcon.classList.remove("text-primary");
    } else {
      navbar.classList.remove("bg-primary");
      const navlinkList = [...navlinks].map((el) => {
        el.classList.remove("navbar-link-secondary");
      });
      navbarToggler.classList.add("navbar-toggler-primary");
      navbarToglerIcon.classList.add("text-primary");
    }
  });
}

document.addEventListener("DOMContentLoaded", userScroll);

const reservar = () => {
  const fecha = document.getElementById("fecha_evento").value;
  const nombre = document.getElementById("nombre_interesado").value;
  const correo = document.getElementById("correo_interesado").value;
  const telefono_interesado = document.getElementById(
    "telefono_interesado"
  ).value;
  const tipoEvento = document.getElementById("tipo_evento").value;
  const espacio = document.getElementById("espacio_salon").value;
  const personas = document.getElementById("numero_personas").value;
  const horario = document.getElementById("horario_evento").value;

  // Your JSON data
  const jsonData = {
    Nombre: nombre,
    Correo: correo,
    Telefono: telefono_interesado,
    FechaEvento: fecha,
    NumeroPersonas: personas,
    EventoTipo: tipoEvento,
    Horario: horario,
  };

  const options = {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(jsonData),
  };

  document.getElementById("btnReservar").disabled = true;

  fetch("http://localhost:7093/api/Reservar", options)
    .then((response) => {
      if (!response.ok) {
        throw new Error("Ocurrió un error al reservar.");
      }
      return response.json();
    })
    .then((data) => {
      mostrarMensajeReserva();
      document.getElementById("btnReservar").disabled = false;
    })
    .catch((error) => {
      console.error("Fetch error:", error);
    });
};

let formRes = document.getElementById("form_reservacion");
formRes.addEventListener("submit", (e) => {
  e.preventDefault();
  ocultarMensajeReserva();

  reservar();
});

const ocultarMensajeReserva = () => {
  const msg = document.getElementById("MensajeReserva");
  if (!msg.classList.contains("d-none")) {
    msg.classList.add("d-none");
  }
};

const mostrarMensajeReserva = () => {
  const msg = document.getElementById("MensajeReserva");
  if (msg.classList.contains("d-none")) {
    msg.classList.remove("d-none");
  }
};
