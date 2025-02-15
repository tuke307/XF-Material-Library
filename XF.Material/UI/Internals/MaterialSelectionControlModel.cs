﻿using System;
using Microsoft.Maui;
using XF.Material.Maui.Utilities;

namespace XF.Material.Maui.UI.Internals
{
    internal class MaterialSelectionControlModel : PropertyChangeAware
    {
        private int _index;
        public int Index
        {
            get => _index;
            set => Set(ref _index, value);
        }

        private string _text;
        public string Text
        {
            get => _text;
            set => Set(ref _text, value);
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set => Set(ref _isSelected, value);
        }

        private double _horizontalSpacing;
        public double HorizontalSpacing
        {
            get => _horizontalSpacing;
            set => Set(ref _horizontalSpacing, value);
        }

        private Command<bool> _selectedChangeCommand;
        public Command<bool> SelectedChangeCommand
        {
            get => _selectedChangeCommand;
            set => Set(ref _selectedChangeCommand, value);
        }

        private string _fontFamily;
        public string FontFamily
        {
            get => _fontFamily;
            set => Set(ref _fontFamily, value);
        }

        private double _fontSize;
        public double FontSize
        {
            get => _fontSize;
            set => Set(ref _fontSize, value);
        }

        private Color _selectedColor;
        public Color SelectedColor
        {
            get => _selectedColor;
            set => Set(ref _selectedColor, value);
        }

        private Color _unselectedColor;
        public Color UnselectedColor
        {
            get => _unselectedColor;
            set => Set(ref _unselectedColor, value);
        }

        private Color _textColor;
        public Color TextColor
        {
            get => _textColor;
            set => Set(ref _textColor, value);
        }

        private double _verticalSpacing;
        public double VerticalSpacing
        {
            get => _verticalSpacing;
            set => Set(ref _verticalSpacing, value);
        }

        private bool _canBeUnselected = true;
        public bool CanBeUnselected
        {
            get => _canBeUnselected;
            set => Set(ref _canBeUnselected, value);
        }

        public override int GetHashCode()
        {
            return Index;
        }

        public override bool Equals(object obj)
        {
            if (obj is MaterialSelectionControlModel model)
            {
                return Index == model.Index;
            }

            return false;
        }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
