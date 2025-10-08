Feature: Registro como Administrador
  Como administrador, quiero registrarme en la aplicación web,
  para hacer uso de las características disponibles.

  Scenario: Registro exitoso del administrador
    Given que el administrador ha completado el formulario con los siguientes datos:
      | Email           | Password |
      | admin@upc.edu.pe | admin123  |
    When el administrador envía la solicitud de registro
    Then el sistema debe validar los datos ingresados
    And el sistema debe mostrar un mensaje de éxito indicando que el registro ha sido exitoso

  Scenario: Falta de campos obligatorios en el registro
    Given que el administrador ha dejado campos obligatorios del formulario sin completar:
      | Email | Password |
      |       |         |
    When intenta enviar la solicitud de registro
    Then el sistema debe mostrar mensajes de error en los campos faltantes
