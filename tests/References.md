# paso1: Variables de conveniencia
$DOTNET = "$env:USERPROFILE\dotnet\dotnet.exe"
$TESTPROJ = "tests/Eduspace.Api.IntegrationTests"



# Listar todas las pruebas detectadas
& $DOTNET test $TESTPROJ --list-tests

# Ejecutar TODAS las pruebas
& $DOTNET test $TESTPROJ

# ===== Por CLASE =====
# MeetingsControllerIT (2 integrales)
& $DOTNET test $TESTPROJ --filter "FullyQualifiedName~Eduspace.Api.IntegrationTests.MeetingsControllerIT"

# ClassroomsControllerIT (2 integrales)
& $DOTNET test $TESTPROJ --filter "FullyQualifiedName~Eduspace.Api.IntegrationTests.ClassroomsControllerIT"

# SharedAreaControllerIT (2 integrales)
& $DOTNET test $TESTPROJ --filter "FullyQualifiedName~Eduspace.Api.IntegrationTests.SharedAreaControllerIT"

# ResourcesControllerIT (2 integrales)
& $DOTNET test $TESTPROJ --filter "FullyQualifiedName~Eduspace.Api.IntegrationTests.ResourcesControllerIT"

# ReservationsEndpointsTests 
& $DOTNET test $TESTPROJ --filter "FullyQualifiedName~Eduspace.Api.IntegrationTests.ReservationsEndpointsTests"

# MeetingsController unit tests con Moq (para correr aparte)
& $DOTNET test $TESTPROJ --filter "FullyQualifiedName~Eduspace.Api.IntegrationTests.Controllers.MeetingsControllerTests"

# ===== Por MÉTODO  =====
# MeetingsControllerIT
& $DOTNET test $TESTPROJ --filter "FullyQualifiedName=Eduspace.Api.IntegrationTests.MeetingsControllerIT.GetAllMeetings_EmptyDb_ReturnsOk_AndEmptyArray"

& $DOTNET test $TESTPROJ --filter "FullyQualifiedName=Eduspace.Api.IntegrationTests.MeetingsControllerIT.DeleteMeeting_NotExistingId_ReturnsNotFound"

# ClassroomsControllerIT
& $DOTNET test $TESTPROJ --filter "FullyQualifiedName=Eduspace.Api.IntegrationTests.ClassroomsControllerIT.GetAllClassrooms_EmptyDb_ReturnsOk_AndEmptyArray"

& $DOTNET test $TESTPROJ --filter "FullyQualifiedName=Eduspace.Api.IntegrationTests.ClassroomsControllerIT.GetClassroomById_NotExisting_ReturnsNotFound"

# SharedAreaControllerIT
& $DOTNET test $TESTPROJ --filter "FullyQualifiedName=Eduspace.Api.IntegrationTests.SharedAreaControllerIT.GetAllSharedAreas_EmptyDb_ReturnsOk_AndEmptyArray"

& $DOTNET test $TESTPROJ --filter "FullyQualifiedName=Eduspace.Api.IntegrationTests.SharedAreaControllerIT.GetSharedAreaById_NotExisting_ReturnsNotFound"

# ResourcesControllerIT
& $DOTNET test $TESTPROJ --filter "FullyQualifiedName=Eduspace.Api.IntegrationTests.ResourcesControllerIT.GetAllResourcesByClassroomId_EmptyDb_ReturnsOk_AndEmptyArray"

& $DOTNET test $TESTPROJ --filter "FullyQualifiedName=Eduspace.Api.IntegrationTests.ResourcesControllerIT.GetResourceById_NotExistingOrWrongClassroom_ReturnsNotFound"

# ReservationsEndpointsTests 
& $DOTNET test $TESTPROJ --filter "FullyQualifiedName=Eduspace.Api.IntegrationTests.ReservationsEndpointsTests.GetAllReservations_Returns_OK_And_Array"

& $DOTNET test $TESTPROJ --filter "FullyQualifiedName=Eduspace.Api.IntegrationTests.ReservationsEndpointsTests.GetAllReservationsByArea_Returns_OK_And_Array"


# pruebas unitarias

# Ejecuta TODOS los tests del proyecto Core
dotnet test tests/Eduspace.Core.Tests


dotnet test tests/Eduspace.Core.Tests --filter "Name~AccountTests"
dotnet test tests/Eduspace.Core.Tests --filter "Name~ClassroomTests"
dotnet test tests/Eduspace.Core.Tests --filter "Name~MeetingTests"
dotnet test tests/Eduspace.Core.Tests --filter "Name~ReservationTests"
dotnet test tests/Eduspace.Core.Tests --filter "Name~TeacherProfileTests"
# solo de prueba
dotnet test tests/Eduspace.Core.Tests --filter "Name~SmokeTests"



