module Navigation

open Feliz
open Fable.ReactQuery

type Example =
    | Simple
    | Basic

[<ReactComponent>]
let Navigation () =
    let queryClient = QueryClient []
    let (example, setExample) = React.useState (Basic)

    QueryClientProvider
        queryClient
        [ Html.div [
              Html.button [
                  prop.text "Simple"
                  prop.onClick (fun _ -> setExample (Simple))
              ]
              Html.button [
                  prop.text "Basic"
                  prop.onClick (fun _ -> setExample (Basic))
              ]
              match example with
              | Simple -> Simple.Example()
              | Basic -> Basic.Example()
          ]
          ReactQueryDevtools [
              InitialIsOpen false
          ] ]