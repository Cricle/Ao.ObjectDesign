namespace Ao.ObjectDesign.Wpf
{
    public interface IWpfObjectDesigner
    {
        IWpfDesignDataTemplateBuildResult BuildDataTemplate(DesignMapping mapping, DesignLevels designLevel, AttackModes attackMode);
        IWpfDesignDataTemplateBuildResult BuildDataTemplate(object instance, AttackModes attackMode);
        IWpfDesignBuildUIResult BuildUI(DesignMapping mapping, DesignLevels designLevel, AttackModes attackMode);
        IWpfDesignBuildUIResult BuildUI(object instance, AttackModes attackMode);
    }
}