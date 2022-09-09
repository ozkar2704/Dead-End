VAR gold = 15
VAR hachas = 0
VAR noConoceTrama = 0
VAR caeBien = 0

{ noConoceTrama:
    - 0:
        -> intro
    - 1:
        -> problema
    - 2:
        -> aceptar
}

=== intro ===
(Oyes al verdugo enfurruñado, igual es mejor no hablar con él, parece peligroso)
* [¿Hola?]
    AH HOLA, NO TE HABÍA VISTO CON LO PEQUEÑO QUE ERES
    -> elegir
* [Marcharse]
(El verdugo sigue quejándose)
-> END

=== elegir ===
~ noConoceTrama = 1
OYE, ¿A TI TE PARECE QUE DOY MIEDO?
    * [Mucho]
    ~ caeBien = 1
        ¿EN SERIO? JAJAJJAA ME CAES BIEN
        ->problema
    * [Para nada]
        ¿COMO? NO ME CAES BIEN PARA NADA
        ->problema
        
=== problema ===
AL FINAL SOY UN VERDUGO, TENGO QUE DAR MIEDO EN MI TRABAJO
AUNQUE TENGO OTRO PROBLEMA, HE PERDIDO MI HACHA Y AHORA NO PUEDO TRABAJAR
    * [Dar 15 oro]
        {gold >= 15:
            ¿ORO? ¡¡PERFECTO AHORA ME PUEDO COMPRAR UN HACHA NUEVA!!
            ~ gold = gold - 15
            -> aceptar
        - else:
            NO SEAS TONTO ESO NO ES SUFUCIENTE PARA UN HACHA
            -> END
        }
    * [Dar hacha]
        {hachas > 0:
            ¿Esa es...? ¡¡MI HACHA!! MIL GRACIAS
            ~ hachas = hachas - 1
            -> aceptar
        - else:
            APRECIO EL GESTO, PERO PARA DARME UN HACHA NECESITAS UN HACHA...(idiota)
            -> END
        }
    * [Hasta luego]
        (El verdugo sigue quejándose)
        -> END

=== aceptar ===
~ noConoceTrama = 2
{caeBien == 1:
    COMO ME CAES BIEN TE VOY A DECIR UN SECRETO
    LOS CAMINOS DE LA IZQUIERDA Y ARRIBA SON POR LOS QUE MANDO A LA GENTE A MORIR jejeje...
    -> END
- else:
    COMO ME HAS AYUDADO TE VOY A DECIR QUE HAY DOS CAMINOS NO SEGUROS
    Y UNO DE ELLOS ES EL DE LA IZQUIERDA
    jejeje... se va a morir
    -> END
}