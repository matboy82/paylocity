As I didn't have a DB setup I thought I'd address the potential sql here:

Probably have some tables for each entity, matching the fields (based on current requirements, new requirements would change it)
Dependent
Employee
PayInfo
Deductions

Relationships:
Employee -> Dependent (one to possible many)
Employee -> PayInfo -> Deductions

Procs could do any calculations, but I'd prefer to keep in a Service class with other business logic.