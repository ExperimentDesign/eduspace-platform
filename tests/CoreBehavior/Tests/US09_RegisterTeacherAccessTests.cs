using NUnit.Framework;
using FULLSTACKFURY.EduSpace.Tests.StepDefinitions;
using TechTalk.SpecFlow;

namespace FULLSTACKFURY.EduSpace.Tests.Tests
{
    [TestFixture]
    public class US09_RegisterTeacherAccessTests
    {
        [Test]
        public void RegistroInformacionAccesoCorrecta()
        {
            var steps = new US09_RegisterTeacherAccessSteps();

            var tableCorrecta = new Table("Username", "Password", "ConfirmPassword");
            tableCorrecta.AddRow("juan.p", "123456", "123456");

            steps.GivenFormularioCompletoValido(tableCorrecta);
            steps.WhenDocenteEnviaRegistro();
            steps.ThenSistemaAlmacenaCredenciales();
        }

        [Test]
        public void RegistroInformacionAccesoIncorrecta()
        {
            var steps = new US09_RegisterTeacherAccessSteps();

            var tableIncorrecta = new Table("Username", "Password", "ConfirmPassword");
            tableIncorrecta.AddRow("juan.p", "123456", "654321");

            steps.GivenFormularioCompletoInvalido(tableIncorrecta);
            steps.WhenDocenteEnviaRegistro();
            steps.ThenSistemaMuestraError();
        }
    }
}