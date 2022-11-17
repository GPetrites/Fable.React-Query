namespace Fable.ReactQuery

open Fable.Core.JS
open Fable.Core.JsInterop
open Fable.Core

type Status =
    | Loading
    | Error
    | Success

type IError = { message: string }

type IQueryInternal<'TData> =
    { isLoading: bool
      isFetching: bool
      data: 'TData
      status: string
      error: IError }

type IQuery<'TData> =
    { isLoading: bool
      isFetching: bool
      data: 'TData
      status: Status
      error: IError }

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

        let qry: IQueryInternal<'TData> = import "useQuery" "@tanstack/react-query" props
        console.log (qry)

        { isLoading = qry.isLoading
          isFetching = qry.isFetching
          data = qry.data
          status =
            match qry.status with
            | "loading" -> Loading
            | "error" -> Error
            | _ -> Success
          error = qry.error }

    static member inline useQuery<'TData>((queryKey: string), (queryFn: unit -> Promise<'TData>)) =
        Query.internalUseQuery queryKey queryFn

    static member inline useQuery<'TKey1, 'TData>
        (
            (queryKey: string, key1: 'TKey1),
            (queryFn: 'TKey1 -> Promise<'TData>)
        ) =
        Query.internalUseQuery queryKey (fun () -> queryFn key1)

    static member inline useQuery<'TData>((queryKey: string), (queryFn: unit -> Async<'TData>)) =
        Query.internalUseQuery queryKey (queryFn >> Async.StartAsPromise)