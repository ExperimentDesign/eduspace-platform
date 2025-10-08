Feature: US11 - Registro de espacios compartidos
  Como administrador
  Quiero registrar los espacios compartidos
  Para que puedan ser gestionados en la plataforma web

  Scenario: Registro exitoso de espacios compartidos
    Given que el administrador ha completado el formulario de registro de espacio compartido con datos válidos
      | Name          | Capacity | Description                |
      | Sala de estudio | 20     | Espacio para trabajo grupal |
    When el administrador envía el registro de espacio compartido
    Then el sistema debe almacenar correctamente el espacio compartido

  Scenario: Información incorrecta de espacios compartidos
    Given que el administrador ha completado el formulario de registro de espacio compartido con datos incompletos
      | Name | Capacity | Description |
      |      |         |             |
    When el administrador envía el registro de espacio compartido
    Then el sistema debe mostrar un mensaje de error
