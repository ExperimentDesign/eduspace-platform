Feature: Registro de horarios para reserva de espacios compartidos
  Como docente
  Quiero registrar el horario en el cual quiero reservar un espacio compartido
  Para asegurar su disponibilidad en el momento deseado
  
  Scenario: Registro de horarios exitoso
    Given que el docente ha llenado todos los campos del formulario de reserva con datos de formato válido
      | AreaId | TeacherId | Start           | End             |
      | 1      | 5         | 2025-10-08 10:00 | 2025-10-08 12:00 |
    When el docente envía la reserva
    Then los datos ingresados del horario se almacenan en la base de datos
    
  Scenario: Información de horarios incorrecto
    Given que el docente ha llenado el campo de reserva con datos de formato inválido
      | AreaId | TeacherId | Start           | End             |
      | 1      | 5         | 2025-10-08 14:00 | 2025-10-08 12:00 |
    When el docente envía la reserva
    Then el sistema muestra un mensaje de error
