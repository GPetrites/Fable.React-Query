module App

open Feliz
open Browser.Dom
open Fable.Core.JsInterop

importSideEffects "./styles/global.scss"

ReactDOM.render (Navigation.Navigation(), document.getElementById "app")