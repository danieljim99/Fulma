namespace Elmish.Bulma.Components.Grids

open Elmish
open Elmish.Bulma.BulmaClasses
open Elmish.Bulma.Common
open Fable.Core
open Fable.Core.JsInterop
open Fable.Helpers.React
open Fable.Helpers.React.Props
open Fable.Import

module Columns =

    module Types =

        type IDisplay =
            | Mobile
            | DesktopOnly

        type ISpacing =
            | IsMultiline
            | IsGapless
            | IsGrid

        type IAlignement =
            | IsCentered
            | IsVCentered

        type Option =
            | Display of IDisplay
            | Spacing of ISpacing
            | Alignment of IAlignement
            | CustomClass of string
            | Props of IHTMLProp list

        let ofAlignment =
            function
            | IsCentered -> bulma.Grid.Columns.Alignment.IsCentered
            | IsVCentered -> bulma.Grid.Columns.Alignment.IsVCentered

        let ofSpacing =
            function
            | IsMultiline -> bulma.Grid.Columns.Spacing.IsMultiline
            | IsGapless -> bulma.Grid.Columns.Spacing.IsGapless
            | IsGrid -> bulma.Grid.Columns.Spacing.IsGrid

        let ofDisplay =
            function
            | Mobile -> bulma.Grid.Columns.Display.OnMobile
            | DesktopOnly -> bulma.Grid.Columns.Display.OnlyDesktop

        type Options =
            { Display : string option
              Spacing : string option
              Alignment : string option
              CustomClass : string option
              Props : IHTMLProp list }
            static member Empty =
                { Display = None
                  Spacing = None
                  Alignment = None
                  CustomClass = None
                  Props = [] }

    open Types

    // Alignment
    let isCentered = Alignment IsCentered
    let isVCentered = Alignment IsVCentered
    // Display
    let onMobile = Display Mobile
    let onDesktopOnly = Display DesktopOnly
    // Spacing
    let isMultiline = Spacing IsMultiline
    let isGapless = Spacing IsGapless
    let isGrid = Spacing IsGrid
    // Extra
    let customClass = CustomClass
    let props = Props

    let columns (options: Option list) children =
        let parseOptions (result: Options) =
            function
            | Display display ->
                { result with Display = ofDisplay display |> Some }

            | Spacing spacing ->
                { result with Spacing = ofSpacing spacing |> Some }

            | Alignment alignment ->
                { result with Alignment = ofAlignment alignment |> Some }

            | CustomClass customClass -> { result with CustomClass = customClass |> Some }

            | Props props -> { result with Props = props }

        let opts = options |> List.fold parseOptions Options.Empty

        div [ yield ClassName ( Helpers.generateClassName bulma.Grid.Columns.Container
                                                    [ opts.Alignment
                                                      opts.Display
                                                      opts.Spacing
                                                      opts.CustomClass ] ) :> IHTMLProp
              yield! opts.Props ]
            children
