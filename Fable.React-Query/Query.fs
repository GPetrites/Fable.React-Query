namespace Fable.ReactQuery

open Fable.Core.JsInterop
open Feliz
open Browser.Dom
open Feliz.ReactApi
open Fable.Core.JS
open System
open Fable.Core

type IQuery<'TData> = { isLoading: bool; data: 'TData }

[<Erase>]
type Query =
    static member inline internalUseQuery<'TData>
        (queryKey: string)
        (queryFn: unit -> Promise<'TData>)
        (opts: obj list)
        : IQuery<'TData> =
        let props =
            createObj [
                "queryKey" ==> [| queryKey |]
                "queryFn" ==> queryFn
            ]

        import "useQuery" "@tanstack/react-query" props

    static member inline useQuery<'TData>((queryKey: string), (queryFn: unit -> Promise<'TData>)) =
        Query.internalUseQuery queryKey queryFn

    static member inline useQuery<'TData>((queryKey: string), (queryFn: unit -> Async<'TData>)) =
        Query.internalUseQuery queryKey (queryFn >> Async.StartAsPromise)