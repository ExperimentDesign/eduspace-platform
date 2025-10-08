Feature: Registro de recursos
  Como administrador
  Quiero registrar los recursos de los salones de clase
  Para mantener un inventario actualizado y optimizar la gestión de los recursos disponibles.

  Scenario: Registro exitoso de un recurso
    Given que el administrador está en la vista Registro de Recursos con datos válidos
      | Name         | KindOfResource | ClassroomId |
      | Proyector    | Electrónico    | 1           |
    When el administrador envía el registro del recurso
    Then el sistema registra el recurso de salón de clase

  Scenario: Registro con datos incompletos
    Given que el administrador está en la vista Registro de Recursos con datos incompletos
      | Name     | KindOfResource | ClassroomId |
      |          | Electrónico    |             |
    When el administrador envía el registro del recurso
    Then el sistema rechaza el registro y muestra un mensaje de error
