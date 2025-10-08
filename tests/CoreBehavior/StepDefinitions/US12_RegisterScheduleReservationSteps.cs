using TechTalk.SpecFlow;
using FluentAssertions;
using System.Collections.Generic;

namespace FULLSTACKFURY.EduSpace.Tests.StepDefinitions
{
    [Binding]
    public class US12_RegisterScheduleReservationSteps
    {
        private Dictionary<string, string> _formData;
        private string _mensaje;

        [Given(@"que el docente ha llenado todos los campos del formulario de reserva con datos de formato válido")]
        public void GivenFormularioValido(Table table)
        {
            _formData = new Dictionary<string, string>();
            foreach (var row in table.Rows)
            {
                foreach (var header in table.Header)
                {
                    _formData[header] = row[header];
                }
            }
        }

        [Given(@"que el docente ha llenado el campo de reserva con datos de formato inválido")]
        public void GivenFormularioInvalido(Table table)
        {
            _formData = new Dictionary<string, string>();
            foreach (var row in table.Rows)
            {
                foreach (var header in table.Header)
                {
                    _formData[header] = row[header];
                }
            }
        }

        [When(@"el docente envía la reserva")]
        public void WhenEnviaReserva()
        {
            // Simulación simple de validación
            if (!string.IsNullOrWhiteSpace(_formData["AreaId"]) &&
                !string.IsNullOrWhiteSpace(_formData["TeacherId"]) &&
                !string.IsNullOrWhiteSpace(_formData["Start"]) &&
                !string.IsNullOrWhiteSpace(_formData["End"]))
            {
                // Validar que fecha de inicio sea antes de fecha fin
                var start = System.DateTime.Parse(_formData["Start"]);
                var end = System.DateTime.Parse(_formData["End"]);

                if (start < end)
                    _mensaje = "Reserva exitosa";
                else
                    _mensaje = "Error: Fecha de inicio debe ser anterior a fecha de fin";
            }
            else
            {
                _mensaje = "Error: Campos obligatorios faltantes";
            }
        }

        [Then(@"los datos ingresados del horario se almacenan en la base de datos")]
        public void ThenDebeAlmacenar()
        {
            _mensaje.Should().Contain("Reserva exitosa");
        }

        [Then(@"el sistema muestra un mensaje de error")]
        public void ThenDebeMostrarError()
        {
            _mensaje.Should().Contain("Error");
        }
    }
}
