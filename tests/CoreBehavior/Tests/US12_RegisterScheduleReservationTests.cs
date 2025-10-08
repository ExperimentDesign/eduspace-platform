using NUnit.Framework;
using FluentAssertions;
using System;
using System.Collections.Generic;

namespace FULLSTACKFURY.EduSpace.Tests
{
    public class US12_RegisterScheduleReservationTests
    {
        private Dictionary<string, string> _formData;
        private string _mensaje;

        private void CompletarFormulario(Dictionary<string, string> datos)
        {
            _formData = datos;
        }

        private void EnviarReserva()
        {
            if (!string.IsNullOrWhiteSpace(_formData["AreaId"]) &&
                !string.IsNullOrWhiteSpace(_formData["TeacherId"]) &&
                !string.IsNullOrWhiteSpace(_formData["Start"]) &&
                !string.IsNullOrWhiteSpace(_formData["End"]))
            {
                DateTime start, end;
                bool startOk = DateTime.TryParse(_formData["Start"], out start);
                bool endOk = DateTime.TryParse(_formData["End"], out end);

                if (startOk && endOk && end > start)
                    _mensaje = "Reserva exitosa";
                else
                    _mensaje = "Error: Horario inválido";
            }
            else
            {
                _mensaje = "Error: Campos obligatorios faltantes";
            }
        }

        [Test]
        public void ReservaExitosa()
        {
            CompletarFormulario(new Dictionary<string, string>
            {
                { "AreaId", "1" },
                { "TeacherId", "5" },
                { "Start", "2025-10-08 10:00" },
                { "End", "2025-10-08 12:00" }
            });

            EnviarReserva();

            _mensaje.Should().Be("Reserva exitosa");
        }

        [Test]
        public void ReservaConHorarioInvalido()
        {
            CompletarFormulario(new Dictionary<string, string>
            {
                { "AreaId", "1" },
                { "TeacherId", "5" },
                { "Start", "2025-10-08 14:00" },
                { "End", "2025-10-08 12:00" }
            });

            EnviarReserva();

            _mensaje.Should().Contain("Error");
        }

        [Test]
        public void ReservaConCamposIncompletos()
        {
            CompletarFormulario(new Dictionary<string, string>
            {
                { "AreaId", "" },
                { "TeacherId", "" },
                { "Start", "" },
                { "End", "" }
            });

            EnviarReserva();

            _mensaje.Should().Contain("Error");
        }
    }
}
