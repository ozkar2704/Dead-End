VAR gold = 0
VAR manzanas = 0
VAR noConoceAlaio = 0

{ noConoceAlaio:
    - 0:
        ~ noConoceAlaio = 1
        -> intro
    - 1:
        -> elegir
    - 2:
        -> aceptar
}

=== intro ===
Hola, me llamo Alaio
¿A tí también te ha transportado la ruptura aquí?
Yo esta zona la conozco y se volver pero tú deberías tener cuidado,
muchos caminos llevan a zonas de las que es imposible regresar
-> elegir

=== elegir ===
Si me das una manzana te puedo decir cual es el primer cruce, tengo mucha hambre...
* [Dar manzana]
    {manzanas > 0:
        Muchas gracias!! Me estaba muriendo de hambre
        ~ manzanas = manzanas - 1
        -> aceptar
    - else:
        Vaya tu tampoco tienes manzanas, jo que hambre...
        -> END
    }
* [Dar 5 oro]
    {gold >= 5:
        ¿5 monedas? bueno supongo que me puedo comprar una mazana con ellas
        ~ gold = gold -5
        -> aceptar
    - else:
        ¿Monedas? ah pero no tienes suficientes, jo que hambre
        -> END
    }
* [Hasta luego]
Agur
-> END

=== aceptar ===
~ noConoceAlaio = 2
El camino correcto es el de la izquierda, buena suerte!
-> END