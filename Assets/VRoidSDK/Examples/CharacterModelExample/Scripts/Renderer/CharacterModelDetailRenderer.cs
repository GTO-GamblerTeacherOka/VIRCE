using System.Collections.Generic;
using Pixiv.VroidSdk.Api.DataModel;
using Pixiv.VroidSdk.Localize;
using Pixiv.VroidSdk.Decorator;
using UnityEngine;
using VRoidSDK.Examples.Core.Renderer;
using VRoidSDK.Examples.Core.View;
using VRoidSDK.Examples.CharacterModelExample.Model;
using VRoidSDK.Examples.CharacterModelExample.View;
using VRoidSDK.Examples.Core.Localize;

namespace VRoidSDK.Examples.CharacterModelExample.Renderer
{
    public class CharacterModelDetailRenderer : IRenderer
    {
        private bool _isActive;
        private string _characterName;
        private string _characterModelName;
        private string _characterPublisherName;
        private CharacterLicense _license;
        private CharacterLicenseVRM10 _licenseVRM10;
        private WebImage _portraitImage;
        private string _specVersion;
        private bool _licenseAccepted;
        private bool _isLoaded;

        public CharacterModelDetailRenderer(CharacterModelDetailModel model)
        {
            if (model.CharacterModel == null)
            {
                return;
            }

            _specVersion = model.CharacterModel?.latest_character_model_version.spec_version;
            if (_specVersion == "1.0")
            {
                var vrm10Meta = model.CharacterModel?.latest_character_model_version.vrm_meta;
                _licenseVRM10 = new CharacterLicenseVRM10(vrm10Meta);
            }
            else
            {
                _license = model.CharacterModel?.license;
            }

            _characterName = model.CharacterModel?.character.name;
            _characterModelName = model.CharacterModel?.name;
            _characterPublisherName = model.CharacterModel?.character.user.name;
            _portraitImage = model.CharacterModel?.portrait_image.sq150;

            _isActive = model.Active;
            _licenseAccepted = model.IsLicenseAccepted;
        }

        public CharacterModelDetailRenderer(LoadedCharacterModelModel model)
        {
            if (model.CharacterModel == null)
            {
                return;
            }

            _specVersion = model.CharacterModel?.latest_character_model_version.spec_version;
            if (_specVersion == "1.0")
            {
                var vrm10Meta = model.CharacterModel?.latest_character_model_version.vrm_meta;
                _licenseVRM10 = new CharacterLicenseVRM10(vrm10Meta);
            }
            else
            {
                _license = model.CharacterModel?.license;
            }

            _characterName = model.CharacterModel?.character.name;
            _characterModelName = model.CharacterModel?.name;
            _characterPublisherName = model.CharacterModel?.character.user.name;
            _portraitImage = model.CharacterModel?.portrait_image.sq150;

            _isActive = model.Active;
            _isLoaded = true;
        }

        public void Rendering(RootView root)
        {
            if (!_isActive)
            {
                RenderingHiding(root);
                return;
            }

            if (_specVersion == "1.0")
            {
                RenderingVRM10(root);
            }
            else
            {
                RenderingVRM00(root);
            }
        }

        private void RenderingHiding(RootView root)
        {
            var characterModelRoot = (CharacterModelRootView)root;
            characterModelRoot.overlay.Active = false;
            var detailView = characterModelRoot.characterModelDetailView;
            detailView.Active = false;
            var detailViewVRM10 = characterModelRoot.characterModelDetailViewVRM10;
            detailViewVRM10.Active = false;
        }

        private void RenderingVRM00(RootView root)
        {
            var characterModelRoot = (CharacterModelRootView)root;
            var detailView = characterModelRoot.characterModelDetailView;
            characterModelRoot.overlay.Active = true;
            detailView.Active = true;

            Debug.Assert(_license != null, "The license information for VRM0.0 model could not be found.");

            detailView.characterModelIcon.Load(_portraitImage);
            detailView.characterName.Text = _characterName;
            detailView.characterModelName.Text = _characterModelName;
            detailView.characterModelPublisherName.Text = _characterPublisherName;
            detailView.modelUseConditions.Text = Translator.Lang.Get(Key.LicenseTextTitle);
            detailView.vrmFormat.Text = Translator.Lang.Get(Key.LicenseTextFormat) + ": <b>VRM0.0</b>";
            detailView.canUseAvatar.Text = Translator.Lang.Get(Key.LicenseTextCanUseAvatar) + "：" + LicenseTypeText(_license.WhatCanUseAvatar());
            detailView.canUseViolence.Text = Translator.Lang.Get(Key.LicenseTextCanUseViolence) + "：" + LicenseTypeText(_license.WhatCanUseViolence());
            detailView.canUseSexuality.Text = Translator.Lang.Get(Key.LicenseTextCanUseSexuality) + "：" + LicenseTypeText(_license.WhatCanUseSexuality());
            detailView.canUseCorporateCommercial.Text = Translator.Lang.Get(Key.LicenseTextCanUseCorporateCommercial) + "：" + LicenseTypeText(_license.WhatCanUseCorporateCommercial());
            detailView.canUsePersonalCommercial.Text = Translator.Lang.Get(Key.LicenseTextCanUsePersonalCommercial) + "：" + LicenseTypeText(_license.WhatCanUsePersonalCommercial());
            detailView.canModify.Text = Translator.Lang.Get(Key.LicenseTextCanModify) + "：" + LicenseTypeText(_license.WhatModification());
            detailView.canRedistribute.Text = Translator.Lang.Get(Key.LicenseTextCanRedistribute) + "：" + LicenseTypeText(_license.WhatRedistribution());
            detailView.showCredit.Text = Translator.Lang.Get(Key.LicenseTextShowCredit) + "：" + LicenseTypeText(_license.WhatShowCredit());

            if (!_isLoaded)
            {
                detailView.acceptButton.Active = true;
                detailView.acceptButton.Enable = _licenseAccepted;
                detailView.acceptButton.Message.Text = Translator.Lang.Get(ExampleViewKey.ViewCharacterModelDetailModelUse);
                detailView.isAccepted.Active = true;
                detailView.isAccepted.Checked = _licenseAccepted;
                detailView.isAccepted.Message.Text = Translator.Lang.Get(ExampleViewKey.ViewCharacterModelDetailAcceptLicense);
                detailView.cancelButton.Active = true;
                detailView.cancelButton.Message.Text = Translator.Lang.Get(ExampleViewKey.ViewCharacterModelDetailModelUseCancel);
            }
            else
            {
                detailView.acceptButton.Active = false;
                detailView.isAccepted.Active = false;
                detailView.cancelButton.Active = true;
                detailView.cancelButton.Message.Text = Translator.Lang.Get(ExampleViewKey.ViewCharacterModelDetailModelUseCancel);
            }
        }

        private void RenderingVRM10(RootView root)
        {
            var characterModelRoot = (CharacterModelRootView)root;
            var detailView = characterModelRoot.characterModelDetailViewVRM10;
            characterModelRoot.overlay.Active = true;
            detailView.Active = true;

            Debug.Assert(_licenseVRM10 != null, "The license information for VRM1.0 model could not be found.");

            detailView.characterModelIcon.Load(_portraitImage);
            detailView.characterName.Text = _characterName;
            detailView.characterModelName.Text = _characterModelName;
            detailView.characterModelPublisherName.Text = _characterPublisherName;
            detailView.modelUseConditions.Text = Translator.Lang.Get(Key.LicenseTextTitle);
            detailView.vrmFormat.Text = Translator.Lang.Get(Key.LicenseTextFormat) + ": <b>VRM1.0</b>";
            detailView.canUseAvatarByOtherUser.Text = Translator.Lang.Get(Key.Vrm10LicenseTextCanUseAvatar) + "：" + LicenseTypeText(_licenseVRM10.WhatCanUseAvatarByOtherUser());
            detailView.canUseViolence.Text = Translator.Lang.Get(Key.Vrm10LicenseTextCanUseViolence) + "：" + LicenseTypeText(_licenseVRM10.WhatCanUseViolence());
            detailView.canUseSexuality.Text = Translator.Lang.Get(Key.Vrm10LicenseTextCanUseSexuality) + "：" + LicenseTypeText(_licenseVRM10.WhatCanUseSexuality());
            detailView.canUseReligionOrPolitical.Text = Translator.Lang.Get(Key.Vrm10LicenseTextCanUseReligionPolitics) + "：" + LicenseTypeText(_licenseVRM10.WhatCanUseReligionOrPolitical());
            detailView.canUseAntisocialOrHatred.Text = Translator.Lang.Get(Key.Vrm10LicenseTextCanUseAntisocialHatred) + "：" + LicenseTypeText(_licenseVRM10.WhatCanUseAntisocialOrHatred());
            detailView.canUseCommercial.Text = Translator.Lang.Get(Key.Vrm10LicenseTextCanUseCommercial) + "：" + LicenseTypeText(_licenseVRM10.WhatCanUseCommercial());
            detailView.canUseCorporate.Text = Translator.Lang.Get(Key.Vrm10LicenseTextCanUseCorporate) + "：" + LicenseTypeText(_licenseVRM10.WhatCanUseCorporate());
            detailView.canRedistribute.Text = Translator.Lang.Get(Key.Vrm10LicenseTextCanRedistribute) + "：" + LicenseTypeText(_licenseVRM10.WhatRedistribution());
            detailView.canModify.Text = Translator.Lang.Get(Key.Vrm10LicenseTextCanModify) + "：" + LicenseTypeText(_licenseVRM10.WhatModification());
            detailView.canRedistributeModified.Text = Translator.Lang.Get(Key.Vrm10LicenseTextCanRedistributeModified) + "：" + LicenseTypeText(_licenseVRM10.WhatCanRedistributeModified());
            detailView.showCredit.Text = Translator.Lang.Get(Key.Vrm10LicenseTextShowCredit) + "：" + LicenseTypeText(_licenseVRM10.WhatShowCredit());

            if (!_isLoaded)
            {
                detailView.acceptButton.Active = true;
                detailView.acceptButton.Enable = _licenseAccepted;
                detailView.acceptButton.Message.Text = Translator.Lang.Get(ExampleViewKey.ViewCharacterModelDetailModelUse);
                detailView.isAccepted.Active = true;
                detailView.isAccepted.Checked = _licenseAccepted;
                detailView.isAccepted.Message.Text = Translator.Lang.Get(ExampleViewKey.ViewCharacterModelDetailAcceptLicense);
                detailView.cancelButton.Active = true;
                detailView.cancelButton.Message.Text = Translator.Lang.Get(ExampleViewKey.ViewCharacterModelDetailModelUseCancel);
            }
            else
            {
                detailView.acceptButton.Active = false;
                detailView.isAccepted.Active = false;
                detailView.cancelButton.Active = true;
                detailView.cancelButton.Message.Text = Translator.Lang.Get(ExampleViewKey.ViewCharacterModelDetailModelUseCancel);
            }
        }

        private string LicenseTypeText(EnumLicense enumLicense)
        {
            switch (enumLicense)
            {
                case EnumLicense.ok:
                    {
                        var decorator = new TextDecorator(Translator.Lang.Get(Key.LicenseTypeOk));
                        return decorator.Color("#B1CC29").Bold().Text;
                    }
                case EnumLicense.ng:
                    {
                        var decorator = new TextDecorator(Translator.Lang.Get(Key.LicenseTypeNg));
                        return decorator.Color("#ADADAD").Bold().Text;
                    }
                case EnumLicense.need:
                    {
                        var decorator = new TextDecorator(Translator.Lang.Get(Key.LicenseTypeNeed));
                        return decorator.Color("#FF2B00").Bold().Text;
                    }
                case EnumLicense.noneed:
                    {
                        var decorator = new TextDecorator(Translator.Lang.Get(Key.LicenseTypeNoNeed));
                        return decorator.Color("#5C5C5C").Bold().Text;
                    }
                case EnumLicense.profit:
                    {
                        var decorator = new TextDecorator(Translator.Lang.Get(Key.LicenseTypeProfit));
                        return decorator.Color("#B1CC29").Bold().Text;
                    }
                case EnumLicense.nonprofit:
                    {
                        var decorator = new TextDecorator(Translator.Lang.Get(Key.LicenseTypeNonProfit));
                        return decorator.Color("#B1CC29").Bold().Text;
                    }
                case EnumLicense.notset:
                    {
                        var decorator = new TextDecorator(Translator.Lang.Get(Key.LicenseTypeNotSet));
                        return decorator.Color("#ADADAD").Bold().Text;
                    }
                default:
                    return "";
            }
        }
    }
}
