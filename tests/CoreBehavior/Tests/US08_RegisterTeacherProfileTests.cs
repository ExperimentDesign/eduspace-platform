using NUnit.Framework;
using FULLSTACKFURY.EduSpace.Tests.StepDefinitions;
using TechTalk.SpecFlow;

namespace FULLSTACKFURY.EduSpace.Tests.Tests
{
    [TestFixture]
    public class US08_RegisterTeacherProfileTests
    {
        [Test]
        public void RegistroInformacionCorrecta()
        {
            var steps = new US08_RegisterTeacherProfileSteps();

            var tableCorrecta = new Table("Username", "Password", "Nombre", "Apellido", "Email", "Telefono", "AreaAcademica");
            tableCorrecta.AddRow("juan.p", "123456", "Juan", "Perez", "juan.perez@upc.edu.pe", "987654321", "Matemáticas");

            steps.GivenAdminIngresaDatosCompletos(tableCorrecta);
            steps.WhenAdminEnviaRegistro();
            steps.ThenSistemaAlmacenaDatos();
        }

        [Test]
        public void RegistroInformacionIncompleta()
        {
            var steps = new US08_RegisterTeacherProfileSteps();

            var tableIncompleta = new Table("Username", "Password", "Nombre", "Apellido", "Email", "Telefono", "AreaAcademica");
            tableIncompleta.AddRow("juan.p", "", "Juan", "", "", "", "");

            steps.GivenAdminIngresaDatosIncompletos(tableIncompleta);
            steps.WhenAdminEnviaRegistroIncompleto();
            steps.ThenSistemaMuestraError();
        }
    }
}