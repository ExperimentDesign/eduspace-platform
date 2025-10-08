Feature: US07 - Inicio de sesión administrador
  Como administrador
  Quiero iniciar sesión en la aplicación
  Para gestionar la administración de ambientes y recursos

  Scenario: Inicio de sesión exitoso
    Given que el administrador ha completado el formulario de inicio de sesión con los siguientes datos:
      | Email                | Password   |
      | admin@upc.edu.pe    | admin123   |
    When el administrador envía la solicitud de inicio de sesión
    Then el sistema debe autenticar al administrador correctamente
    And el sistema debe permitir al administrador acceder a las funcionalidades para gestionar la administración

  Scenario: Manejo de errores de inicio de sesión
    Given que el administrador ha completado el formulario de inicio de sesión con los siguientes datos:
      | Email                | Password     |
      | admin@upc.edu.pe    | wrongpass    |
    When el administrador intenta iniciar sesión
    Then el sistema debe mostrar un mensaje de error indicando que las credenciales no son válidas
