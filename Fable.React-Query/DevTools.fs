namespace Fable.ReactQuery

open Fable.Core
open Fable.Core.JsInterop
open Feliz
open Feliz.ReactApi

type DevTools = InitialIsOpen of bool

[<AutoOpen>]
module DevTools =
    let ReactQueryDevtools (props: DevTools list) =
        let reactApi: IReactApi = importDefault "react"

        Interop.reactApi.createElement (
            import "ReactQueryDevtools " "@tanstack/react-query-devtools",
            (keyValueList CaseRules.LowerFirst props)
        )