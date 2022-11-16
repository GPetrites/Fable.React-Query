module Simple
open Feliz

[<ReactComponent>]
let Example () =
    Html.div "Simple Example"

[<ReactComponent>]
let App () =
    Example ()
