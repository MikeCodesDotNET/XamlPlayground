﻿using System.Runtime.InteropServices;
using Avalonia.Xaml.Interactivity;
using AvaloniaEdit;
using AvaloniaEdit.Highlighting;
using AvaloniaEdit.TextMate;
using AvaloniaEdit.TextMate.Grammars;

namespace XamlPlayground.Behaviors;

public class TextEditorBehavior : Behavior<TextEditor>
{
    private TextEditor? _textEditor;
    private RegistryOptions? _registryOptions;
    private TextMate.Installation? _textMateInstallation;

    protected override void OnAttached()
    {
        base.OnAttached();

        if (AssociatedObject is { } textEditor)
        {
            _textEditor = textEditor;

            // TODO: Enable for WebAssembly
            // https://github.com/danipen/TextMateSharp/issues/9
            // https://github.com/AvaloniaUI/AvaloniaEdit/issues/201
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Create("BROWSER")))
            {
                _registryOptions = new RegistryOptions(ThemeName.LightPlus);
                _textMateInstallation = _textEditor.InstallTextMate(_registryOptions);
                _textMateInstallation.SetGrammar(_registryOptions.GetScopeByLanguageId(_registryOptions.GetLanguageByExtension(".xml").Id));
            }
            else
            {
                _textEditor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("XML");
            }
        }
    }
}
