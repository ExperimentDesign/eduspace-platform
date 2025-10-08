Feature: US09 - Registro de información de acceso del docente
  Como administrador
  Quiero registrar la información de acceso del docente
  Para que puedan iniciar sesión en la plataforma web

  Scenario: Información de acceso exitosa
    Given que el formulario de registro del docente fue completado con datos de formato válido
      | Username   | Password | ConfirmPassword |
      | juan.p     | 123456   | 123456          |
    When el docente envía el registro de información
    Then el sistema almacenará las credenciales del docente en la base de datos

  Scenario: Información de acceso incorrecta
    Given que el formulario de registro del docente fue completado con datos de formato inválido
      | Username   | Password | ConfirmPassword |
      | juan.p     | 123456   | 654321          |
    When el docente envía el registro de información
    Then el sistema mostrará un mensaje de error
