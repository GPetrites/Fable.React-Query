namespace Fable.ReactQuery

open Fable.Core.JsInterop
open Feliz
open Feliz.ReactApi
open Fable.Core

type DevTools = InitialIsOpen of bool

[<AutoOpen>]
module DevTools =
    let ReactQueryDevtools (props: DevTools list) =
        let reactApi: IReactApi = importDefault "react"

        Interop.reactApi.createElement (
            import "ReactQueryDevtools " "@tanstack/react-query-devtools",
            (keyValueList CaseRules.LowerFirst props)
        )