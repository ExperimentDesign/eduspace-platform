Feature: US06 - Inicio de sesión como profesor
  Como profesor
  Quiero iniciar sesión en la aplicación
  Para acceder a las funcionalidades específicas para docentes

  Scenario: Inicio de sesión exitoso
    Given que el docente ha completado el formulario de inicio de sesión con los siguientes datos:
      | Email                   | Password |
      | profesor@upc.edu.pe    | 123456   |
    When el docente envía la solicitud de inicio de sesión
    Then el sistema debe autenticar que las credenciales del docente sean correctas
    And el sistema debe permitir al docente acceder a las características y recursos específicos para su rol

  Scenario: Manejo de errores de inicio de sesión
    Given que el docente ha completado el formulario de inicio de sesión con los siguientes datos:
      | Email                   | Password      |
      | profesor@upc.edu.pe    | wrongpassword |
    When el docente intenta iniciar sesión
    Then el sistema debe mostrar un mensaje de error indicando que las credenciales son inválidas
