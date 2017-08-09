'   This class holds the Emami data. The data is accessed through three functions that
'   return the limit doses for the whole organ, 2/3 organ and 1/3 organ. At this time, 
'   the dose limits are the 5/5 limits. When either of the three functions is called, it 
'   is called with the argument "organ" which is a string containing the name of the organ of
'   interest. If there is a match in the organ name, the relevant dose limit is then returned 
'   with the function.

Public Class DataEmami
    Dim row(4) As String

    Function whole55(ByVal organ As String) As String
        Dim wholedose55 As String
        wholedose55 = hereisthe55data(organ)(1)
        Return wholedose55
    End Function

    Function twothird55(ByVal organ As String)
        Dim twothirddose55 As String
        twothirddose55 = hereisthe55data(organ)(2)
        Return twothirddose55
    End Function

    Function onethird55(ByVal organ As String)
        Dim onethirddose55 As String
        onethirddose55 = hereisthe55data(organ)(3)
        Return onethirddose55
    End Function

    Function hereisthe55data(ByVal organ As String)
        If organ = "Bladder" Then row = {"Bladder", "6500", "8000", "--"}
        If organ = "Brachial Plexus" Then row = {"Brachial Plexus", "6000", "6100", "6200"}
        If organ = "Brain" Then row = {"Brain", "4500", "5000", "6000"}
        If organ = "Brainstem" Then row = {"Brainstem", "5000", "5300", "6000"}
        If organ = "Cauda Equina" Then row = {"Cauda Equina", "6000", "--", "--"}
        If organ = "Colon" Then row = {"Colon", "4500", "--", "5500"}
        If organ = "Ear (acute)" Then row = {"Ear (acute serous otitis)", "3000", "3000", "3000"}
        If organ = "Ear (chronic)" Then row = {"Ear (chronic serous otitis)", "5500", "5500", "5500"}
        If organ = "Esophagus" Then row = {"Esophagus", "5500", "5800", "6000"}
        If organ = "Esophagus - H&N Plan" Then row = {"Esophagus", "5500", "5800", "6000"}
        If organ = "Femoral Head" Then row = {"Femoral Head", "5200", "--", "--"}
        If organ = "Heart" Then row = {"Heart", "4000", "4500", "6000"}
        If organ = "Heart - Breast Plan" Then row = {"Heart", "4000", "4500", "6000"}
        If organ = "Kidney" Then row = {"Kidney", "2300", "3000", "5000"}
        If organ = "Kidneys - Bilateral" Then row = {"Kidney", "2300", "3000", "5000"}
        If organ = "Larynx" Then row = {"Larynx (Necrosis)", "7000", "7000", "7900"}
        If organ = "Larynx" Then row = {"Larynx (Edema)", "4500", "4500", "--"}
        If organ = "Lens" Then row = {"Lens", "1000", "--", "--"}
        If organ = "Liver" Then row = {"Liver", "3000", "3500", "5000"}
        If organ = "Liver - Primay" Then row = {"Liver", "3000", "3500", "5000"}
        If organ = "Liver - Metastatic" Then row = {"Liver", "3000", "3500", "5000"}
        If organ = "Lung" Then row = {"Lung", "1750", "3000", "4500"}
        If organ = "Lung - Breast Plan" Then row = {"Lung", "1750", "3000", "4500"}
        If organ = "Lung - Single" Then row = {"Lung", "1750", "3000", "4500"}
        If organ = "Lung - Total" Then row = {"Lung", "1750", "3000", "4500"}
        If organ = "Optic Chiasm" Then row = {"Optic Chiasm", "5000", "--", "--"}
        If organ = "Optic Nerve" Then row = {"Optic Nerve", "5000", "--", "--"}
        If organ = "Parotid Gland" Then row = {"Parotid Gland", "3200", "3200", "--"}
        If organ = "Rectum" Then row = {"Rectum", "6000", "--", "--"}
        If organ = "Retina" Then row = {"Retina", "4500", "--", "--"}
        If organ = "Rib Cage" Then row = {"Rib Cage", "--", "--", "5000"}
        If organ = "Skin" Then row = {"Skin (Tealgiectasia)(100 cm2, 30 cm2, 10 cm2)", "5000", "--", "--"}
        If organ = "Skin" Then row = {"Skin (Necrosis, Ulceration)(100 cm2, 30 cm2, 10 cm2)", "5500", "6000", "7000"}
        If organ = "Length of Cord" Then row = {"Spinal Cord (20 cm, 10 cm, 5 cm)", "20 cm", "10 cm", "5 cm"}
        If organ = "Spinal Cord" Then row = {"Spinal Cord (20 cm, 10 cm, 5 cm)", "4700", "5000", "5000"}
        If organ = "Small Intestine" Then row = {"Small Intestine", "4000", "--", "5000"}
        If organ = "Stomach" Then row = {"Stomach", "5000", "5500", "6000"}
        If organ = "TMJ Mandible" Then row = {"TMJ Mandible", "6000", "6000", "6500"}
        If organ = "Thyroid" Then row = {"Thyroid", "4500", "--", "--"}
        Return row
    End Function

    Function whole505(ByVal organ As String) As String
        Dim wholedose505 As String
        wholedose505 = hereisthe505data(organ)(1)
        Return wholedose505
    End Function

    Function twothird505(ByVal organ As String)
        Dim twothirddose505 As String
        twothirddose505 = hereisthe505data(organ)(2)
        Return twothirddose505
    End Function

    Function onethird505(ByVal organ As String)
        Dim onethirddose505 As String
        onethirddose505 = hereisthe505data(organ)(3)
        Return onethirddose505
    End Function
    Function hereisthe505data(ByVal organ As String)
        If organ = "Bladder" Then row = {"Bladder", "8000", "8500", "--"}
        If organ = "Brachial Plexus" Then row = {"Brachial Plexus", "7500", "7600", "7700"}
        If organ = "Brain" Then row = {"Brain", "6000", "6500", "7500"}
        If organ = "Brainstem" Then row = {"Brainstem", "6500", "--", "--"}
        If organ = "Cauda Equina" Then row = {"Cauda Equina", "7500", "--", "--"}
        If organ = "Colon" Then row = {"Colon", "5500", "--", "6500"}
        If organ = "Ear (acute)" Then row = {"Ear (acute serous otitis)", "4000", "4000", "4000"}
        If organ = "Ear (chronic)" Then row = {"Ear (chronic serous otitis)", "6500", "6500", "6500"}
        If organ = "Esophagus" Then row = {"Esophagus", "6800", "7000", "7200"}
        If organ = "Esophagus - H&N Plan" Then row = {"Esophagus", "6800", "7000", "7200"}
        If organ = "Femoral Head" Then row = {"Femoral Head", "6500", "--", "--"}
        If organ = "Heart" Then row = {"Heart", "5000", "5500", "7000"}
        If organ = "Heart - Breast Plan" Then row = {"Heart", "5000", "5500", "7000"}
        If organ = "Kidney" Then row = {"Kidney", "2800", "4000", "--"}
        If organ = "Kidneys - Bilateral" Then row = {"Kidney", "2800", "4000", "--"}
        If organ = "Larynx" Then row = {"Larynx (Necrosis)", "8000", "8000", "9000"}
        If organ = "Larynx" Then row = {"Larynx (Edema)", "8000", "--", "--"}
        If organ = "Lens" Then row = {"Lens", "1800", "--", "--"}
        If organ = "Liver" Then row = {"Liver", "4000", "4500", "5500"}
        If organ = "Liver - Primary" Then row = {"Liver", "4000", "4500", "5500"}
        If organ = "Liver - Metastatic" Then row = {"Liver", "4000", "4500", "5500"}
        If organ = "Lung" Then row = {"Lung", "2450", "4000", "6500"}
        If organ = "Lung - Breast Plan" Then row = {"Lung", "2450", "4000", "6500"}
        If organ = "Lung - Single" Then row = {"Lung", "2450", "4000", "6500"}
        If organ = "Lung - Total" Then row = {"Lung", "2450", "4000", "6500"}
        If organ = "Optic Chiasm" Then row = {"Optic Chiasm", "6500", "--", "--"}
        If organ = "Optic Nerve" Then row = {"Optic Nerve", "6500", "--", "--"}
        If organ = "Parotid Gland" Then row = {"Parotid Gland", "4600", "4600", "--"}
        If organ = "Rectum" Then row = {"Rectum", "8000", "--", "--"}
        If organ = "Retina" Then row = {"Retina", "6500", "--", "--"}
        If organ = "Rib Cage" Then row = {"Rib Cage", "--", "--", "6500"}
        If organ = "Skin" Then row = {"Skin (Tealgiectasia)(100 cm2, 30 cm2, 10 cm2)", "6500", "--", "--"}
        If organ = "Skin" Then row = {"Skin (Necrosis, Ulceration)(100 cm2, 30 cm2, 10 cm2)", "6500", "--", "--"}
        If organ = "Spinal Cord" Then row = {"Spinal Cord (20 cm, 10 cm, 5 cm)", "--", "7000", "7000"}
        If organ = "Small Bowel" Then row = {"Small Intestine", "5500", "--", "6000"}
        If organ = "Stomach" Then row = {"Stomach", "6500", "6700", "7000"}
        If organ = "TMJ Mandible" Then row = {"TMJ Mandible", "7200", "7200", "7700"}
        If organ = "Thyroid" Then row = {"Thyroid", "8000", "--", "--"}
        Return row
    End Function

End Class