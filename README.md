# Microservice Manufactura (MES Cycle) – .NET 8

Estructura base para un microservicio siguiendo **CQRS** y **Event Sourcing**, separando **Command** y **Query** en proyectos independientes.

## Proyectos

- `src/CQRS.Core`: infraestructura genérica (commands, events, event sourcing).
- `src/MES-Cycle.COMMON`: utilidades/contratos compartidos.
- `src/MES-Cycle.CMD.API`: API de escritura.
- `src/MES-Cycle.CMD.DOMAIN`: dominio de escritura.
- `src/MES-Cycle.CMD.INFRASTRUCTURE`: infraestructura de escritura.
- `src/MES-Cycle.QUERY.API`: API de lectura.
- `src/MES-Cycle.QUERY.DOMAIN`: modelo de lectura.
- `src/MES-Cycle.QUERY.INFRASTRUCTURE`: infraestructura de lectura.

## Solución

- `MES-Cycle.sln`

