# Coverage Matrix

| Requirement | Covered By Tests |
|---|---|
| Constructors | StudentConstructor_ValidData_CreatesStudent, KarateClass_EnrollStudent_CreatesEnrollment, Announcement_ValidData_CreatesAnnouncement |
| Property validation | UserConstructor_InvalidEmail_ThrowsException, UserConstructor_TooYoung_ThrowsException, Payment_NegativeAmount_ThrowsException |
| ToString overrides | StudentConstructor_ValidData_CreatesStudent, Instructor_RecordAttendance_CreatesAttendance, Report_GenerateSummary_ReturnsText |
| Exception handling | Attendance_FutureDate_ThrowsException, KarateClass_CapacityExceeded_ThrowsException, Administrator_UnauthorizedSettingChange_ThrowsException |
| Inheritance behavior | UserFactory_CreatesAllUserTypes, Polymorphism_UserToString_IsRoleSpecific |
| Abstraction | Report_GenerateSummary_ReturnsText, Payment_ProcessCash_CompletesPayment |
| Design patterns | UserFactory_CreatesAllUserTypes, Payment_ProcessCard_CompletesPayment |
