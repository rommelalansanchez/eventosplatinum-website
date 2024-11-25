(function () {
  "use strict"; // Add custom JavaScript here
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

  const showMessage = (element) => {
    document.querySelector(element).classList.remove("d-none");
    document.querySelector(element).classList.add("d-block");
  };
  const hideMessage = (element) => {
    document.querySelector(element).classList.remove("d-block");
    document.querySelector(element).classList.add("d-none");
  };

  const reservar = () => {
    showMessage(".loading");
    hideMessage(".sent-message");

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

    var url = "api/Reservar";
    fetch(url, options)
      .then((response) => {
        document.getElementById("btnReservar").disabled = false;
        hideMessage(".loading");
        if (!response.ok) {
          throw new Error("Ocurrió un error al enviar.");
        }
        return response.json();
      })
      .then((data) => {
        if (data.success) {
          showMessage(".sent-message");
        } else {
          throw new Error(
            data.Message
              ? data.Message
              : data.message
              ? data.message
              : "Form submission failed."
          );
        }
      })
      .catch((error) => {
        console.error("Fetch error:", error);
      });
  };

  let formRes = document.getElementById("form_reservacion");
  formRes.addEventListener("submit", (e) => {
    e.preventDefault();
    reservar();
  });

  /**
   * Animation on scroll function and init
   */
  function aosInit() {
    AOS.init({
      duration: 600,
      easing: "ease-in-out",
      once: true,
      mirror: false,
    });
  }
  window.addEventListener("load", aosInit);

  /**
   * Initiate glightbox
   */
  const glightbox = GLightbox({
    selector: ".glightbox",
  });

  /**
   * Init isotope layout and filters
   */
  document.querySelectorAll(".isotope-layout").forEach(function (isotopeItem) {
    let layout = isotopeItem.getAttribute("data-layout") ?? "masonry";
    let filter = isotopeItem.getAttribute("data-default-filter") ?? "*";
    let sort = isotopeItem.getAttribute("data-sort") ?? "original-order";

    let initIsotope;
    imagesLoaded(isotopeItem.querySelector(".isotope-container"), function () {
      initIsotope = new Isotope(
        isotopeItem.querySelector(".isotope-container"),
        {
          itemSelector: ".isotope-item",
          layoutMode: layout,
          filter: filter,
          sortBy: sort,
        }
      );
    });

    isotopeItem
      .querySelectorAll(".isotope-filters li")
      .forEach(function (filters) {
        filters.addEventListener(
          "click",
          function () {
            isotopeItem
              .querySelector(".isotope-filters .filter-active")
              .classList.remove("filter-active");
            this.classList.add("filter-active");
            initIsotope.arrange({
              filter: this.getAttribute("data-filter"),
            });
            if (typeof aosInit === "function") {
              aosInit();
            }
          },
          false
        );
      });
  });
})();
