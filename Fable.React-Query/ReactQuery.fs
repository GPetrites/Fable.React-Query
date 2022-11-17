namespace Fable.ReactQuery

open Fable.Core.JsInterop
open Feliz
open Browser.Dom
open Feliz.ReactApi
open Fable.Core.JS
open System

[<AutoOpen>]
module Query =
    type IQueryClient = obj

    type IQuery<'TData> = {
        isLoading: bool
        data: 'TData
    }

    let QueryClient (props: obj list) : IQueryClient =
        let queryClient = import "QueryClient" "@tanstack/react-query"
        createNew queryClient ()

    let QueryClientProvider (client: IQueryClient) (children: ReactElement list) =
        let reactApi: IReactApi = importDefault "react"
        Interop.reactApi.createElement (
            import "QueryClientProvider" "@tanstack/react-query",
            createObj [
                "client" ==> client
                "children" ==> (reactApi.Children.toArray (Array.ofSeq children))
            ]
        )

    let useQuery<'TData> (queryKey:string) (queryFn: unit -> Promise<'TData>) opts : IQuery<'TData> =
        let props = createObj [
            "queryKey" ==> queryKey
            "queryFn" ==> queryFn
        ]
        import "useQuery" "@tanstack/react-query" props