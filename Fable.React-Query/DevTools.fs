namespace Fable.ReactQuery

open Fable.Core.JsInterop
open Feliz
open Feliz.ReactApi

[<AutoOpen>]
module DevTools =
    let ReactQueryDevtools (opts: obj list) =
        let reactApi: IReactApi = importDefault "react"

        Interop.reactApi.createElement (import "ReactQueryDevtools " "@tanstack/react-query-devtools", null)